using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;

public class ES_control : MonoBehaviour
{
    [Header("Electrical Stimulations")]
    [Space(20)]
    public ElectricalStimulation[] ElectricalStimulations;
    //public int Duration = 10;

    [System.Serializable]
    public struct ElectricalStimulation
    {
        [Tooltip("電流値 (mAでは無いので注意)")]
        [Range(0, 4095)]
        public int Current;

        [Tooltip("周波数")]
        [Range(0, 1023)]
        public int Frequency;

        [Tooltip("刺激波形")]
        public WaveForms WaveForm;

        [Tooltip("刺激波形が矩形波の時のduty比 (0が電流なし、10が定電流)")]
        [Range(0, 10)]
        public int Duty;

        [Tooltip("刺激強度の最大値に達するまでの遷移関数")]
        public TransitionForms TransitionForm;

        [Tooltip("刺激強度の最大値に達するまでの時間 (1周期に掛かる時間のX倍)")]
        [Range(0, 15)]
        public int TransitionDuration;
    }

    public enum WaveForms
    {
        square_bipole, //両極矩形波
        square_positive, //正の片側矩形波
        square_negative, //負の片側矩形波
        direct_positive, //正の直流
        direct_negative, //負の直流
        sin, //Sin波
        whitenoise //whitenoise 追加
        //trapezoidal_positive, //正の片側台形波
        //trapezoidal_negative //負の片側台形波
    }

    public enum TransitionForms
    {
        constant, //遷移なし
        linear, //線形に増加
        smooth, //スムーズに増加 (t(3 - 2 * t))
        lineardecay, //線形に減少
        smoothdecay //スムーズに減少 (smoothと対称ではなく、Cos(0, π/2) + 1で減少していく感じ)
    }

    public GameObject SeeeduinoSerialHandlerObject;
    private SeeeduinoSerialHandler seeeduinoSerialHandler;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //SeeduinoSerialHandlerObject = GameObject.Find("SeeduinoSerialHandler");
        seeeduinoSerialHandler = SeeeduinoSerialHandlerObject.GetComponent<SeeeduinoSerialHandler>();
    }
    

    public void StartElectricalStimulation()
    {
        if (ElectricalStimulations.Length >= 1)
        {
            ElectricalStimulation electricalStimulation = ElectricalStimulations[0];
            //StartCoroutine(Stimulate(electricalStimulation));
            SendElectricalStimulation(electricalStimulation.Current, electricalStimulation.Frequency, (int)electricalStimulation.WaveForm,
                electricalStimulation.Duty, electricalStimulation.TransitionDuration, (int)electricalStimulation.TransitionForm);

        }
    }

/*
    public IEnumerator Stimulate(ElectricalStimulation electricalStimulation)
    {
        SendElectricalStimulation(electricalStimulation.Current, electricalStimulation.Frequency, (int)electricalStimulation.WaveForm,
                electricalStimulation.Duty, electricalStimulation.TransitionDuration, (int)electricalStimulation.TransitionForm);

        Debug.Log("STRAT");

        yield return new WaitForSeconds(Duration);

        Debug.Log("STOP");

        StopElectricalStimulation();
    }
    */

    private void SendElectricalStimulation(int current, int frequency, int wave_form, int duty, int transition_duration, int transition_form)
    {
        char channel = '0';

        byte[] buffer = new byte[8];
        int[] Send1 = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Send2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Send3 = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Send4 = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] Send5 = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int i;

        // 最初の3bitがチャンネル
        if (channel == '1') { Send1[7] = 0; Send1[6] = 0; Send1[5] = 1; }
        else if (channel == '2') { Send1[7] = 0; Send1[6] = 1; Send1[5] = 0; }
        else if (channel == '3') { Send1[7] = 0; Send1[6] = 1; Send1[5] = 1; }
        else if (channel == '0') { Send1[7] = 0; Send1[6] = 0; Send1[5] = 0; }
        else if (channel == '4') { Send1[7] = 1; Send1[6] = 0; Send1[5] = 0; }
        else if (channel == '5') { Send1[7] = 1; Send1[6] = 0; Send1[5] = 1; }
        else if (channel == '6') { Send1[7] = 1; Send1[6] = 1; Send1[5] = 0; }

        // 電流値(正)を送る;1byte目の残り5bitと2byte目の7bit
        for (i = 0; i <= 6; i++)
        {
            Send2[i] = current % 2;
            current = current / 2;
        }
        for (i = 0; i <= 4; i++)
        {
            Send1[i] = current % 2;
            current = current / 2;
        }

        // 周波数を送る;2byte目の残り1bitと3byte目と4byte目の1bit
        Send4[0] = frequency % 2;
        frequency = frequency / 2;
        for (i = 0; i <= 7; i++)
        {
            Send3[i] = frequency % 2;
            frequency = frequency / 2;
        }
        Send2[7] = frequency % 2;
        frequency = frequency / 2;

        // 波形情報を送る; 4byte目の残り3bit
        for (i = 4; i <= 6; i++)
        {
            Send4[i] = wave_form % 2;
            wave_form = wave_form / 2;
        }

        // duty比を送る; 4byte目の残り4bit
        for (i = 0; i <= 3; i++)
        {
            Send4[i] = duty % 2;
            duty = duty / 2;
        }

        // 立ち上げ時間を送る; 5byte目の4bit
        for (i = 4; i <= 7; i++)
        {
            Send5[i] = transition_duration % 2;
            transition_duration = transition_duration / 2;
        }

        // 立ち上げ手法を送る; 5byte目の残り3bit
        for (i = 1; i <= 3; i++)
        {
            Send5[i] = transition_form % 2;
            transition_form = transition_form / 2;
        }

        // 転送データの作成
        buffer[0] = Convert.ToByte(71);
        buffer[1] = Convert.ToByte(Send1[7] * 128 + Send1[6] * 64 + Send1[5] * 32 + Send1[4] * 16 + Send1[3] * 8 + Send1[2] * 4 + Send1[1] * 2 + Send1[0]);
        buffer[2] = Convert.ToByte(Send2[7] * 128 + Send2[6] * 64 + Send2[5] * 32 + Send2[4] * 16 + Send2[3] * 8 + Send2[2] * 4 + Send2[1] * 2 + Send2[0]);
        buffer[3] = Convert.ToByte(Send3[7] * 128 + Send3[6] * 64 + Send3[5] * 32 + Send3[4] * 16 + Send3[3] * 8 + Send3[2] * 4 + Send3[1] * 2 + Send3[0]);
        buffer[4] = Convert.ToByte(Send4[7] * 128 + Send4[6] * 64 + Send4[5] * 32 + Send4[4] * 16 + Send4[3] * 8 + Send4[2] * 4 + Send4[1] * 2 + Send4[0]);
        buffer[5] = Convert.ToByte(Send5[7] * 128 + Send5[6] * 64 + Send5[5] * 32 + Send5[4] * 16 + Send5[3] * 8 + Send5[2] * 4 + Send5[1] * 2 + Send5[0]);

        seeeduinoSerialHandler.SendParameters(buffer);
        Task.Delay(1);
    }

    public void StopElectricalStimulation()
    {
        SendElectricalStimulation(0, 0, 0, 0, 0, 0);
    }
}
