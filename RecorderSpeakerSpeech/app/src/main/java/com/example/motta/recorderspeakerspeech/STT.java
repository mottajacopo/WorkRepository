package com.example.motta.recorderspeakerspeech;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Environment;
import android.os.Handler;
import android.util.Log;
import android.widget.Toast;

import com.ibm.watson.developer_cloud.http.HttpMediaType;
import com.ibm.watson.developer_cloud.speech_to_text.v1.SpeechToText;
import com.ibm.watson.developer_cloud.speech_to_text.v1.model.RecognizeOptions;
import com.ibm.watson.developer_cloud.speech_to_text.v1.model.SpeechRecognitionResults;
import com.ibm.watson.developer_cloud.speech_to_text.v1.websocket.BaseRecognizeCallback;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.util.List;
import java.util.Map;

import  static com.example.motta.recorderspeakerspeech.SupportFunctions.removeChar;



public class STT extends AsyncTask<String, String, Void> {

    private static final String TAG = "STT";
    private Context mContext;
    private String temp;
    private boolean result;

    public STT(Context context){
        mContext = context;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();

        Toast.makeText(mContext,"Start verification", Toast.LENGTH_SHORT).show();
    }

    @Override
    protected Void doInBackground(String... strings) {

        SpeechToText service = new SpeechToText();
        service.setUsernameAndPassword(mContext.getString(R.string.username), mContext.getString(R.string.password));

        String _path = strings[0];
        String _filename = strings[1];
        File file = new File(Environment.getExternalStorageDirectory() + "/" + _path + "/" + _filename);

        try{
            InputStream audio = new FileInputStream(file);

            RecognizeOptions options = new RecognizeOptions.Builder()
                    .audio(audio)
                    .contentType(HttpMediaType.AUDIO_WAV)
                    .interimResults(true)
                    .build();

            service.recognizeUsingWebSocket(options, new BaseRecognizeCallback() {
                @Override
                public void onTranscription(SpeechRecognitionResults transcript) {
                    System.out.println(transcript);

                    boolean result = false;

                    temp = transcript.getResults().get(0).getAlternatives().toString();
                    temp = removeChar(temp);
                    /*
                    for(int i =0; i< transcript.getResults().size(); i++)
                    {
                        temp = transcript.getResults().get(i).getAlternatives().toString();
                        temp = removeChar(temp);
                        //if(temp.contains(mContext.getString(R.string.check_phrase)))
                    }
*/
                }
            });
            //delay 10 sec
            Thread.currentThread().sleep(5000);
            if(temp.equals(mContext.getString(R.string.check_phrase))){
                result = true;
            }
        }
        catch (FileNotFoundException e){
            Log.e(TAG,"File not found");
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

        if(result) {
            Toast.makeText(mContext, "Verification succeeded" + "\n" + "result = " + temp, Toast.LENGTH_LONG).show();
        }
        else{
            Toast.makeText(mContext, "Verification failed" + "\n" + "result = " + temp, Toast.LENGTH_LONG).show();
        }

    }


}

