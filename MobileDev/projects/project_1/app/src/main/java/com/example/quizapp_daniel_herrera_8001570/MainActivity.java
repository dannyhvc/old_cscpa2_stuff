package com.example.quizapp_daniel_herrera_8001570;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.RadioGroup;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {
    String answer = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // get eventful widgets
        Button button = findViewById(R.id.submit);
        TextView response = findViewById(R.id.responseBanner);
        final RadioGroup radioGroup = findViewById(R.id.radioGroup);

        // radio group listener
        radioGroup.setOnCheckedChangeListener((radioGroup1, btn_id) -> {
            View radio_btn = radioGroup1.findViewById(btn_id);

            switch (radioGroup.indexOfChild(radio_btn)) {
                case 0:
                    answer = "So close!";
                    break;
                case 1:
                    answer = "Give it another try!";
                    break;
                case 2:
                    answer = "One More and you Got it!";
                    break;
                case 3:
                    answer = "Correct!!!";
                    break;
            }
            Log.i(" >>> response answer ", answer);
        });

        // submit button listener
        button.setOnClickListener(e -> {
            Log.i(" >>> button state " , "submitted");
            response.setText(answer);
        });

    }//onCreate

}//MainActivity