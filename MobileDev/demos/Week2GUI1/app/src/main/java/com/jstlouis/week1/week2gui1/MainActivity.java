package com.jstlouis.week1.week2gui1;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.CheckBox;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {
    final String TAG = "MainActivityDemo";

    TextView textViewColour;
    CheckBox checkBoxBold;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        textViewColour = findViewById(R.id.textViewColour);
        checkBoxBold = findViewById(R.id.checkBoxBold);
    }

    public void onRadioClick(View view) {
        switch (view.getId()) {
            case R.id.radioButtonRed:
                textViewColour.setText(R.string.colour_red);
                textViewColour.setBackgroundColor(Color.RED);
                Log.i(TAG, "Red Selected");
                break;
            case R.id.radioButtonGreen:
                textViewColour.setText(R.string.colour_green);
                textViewColour.setBackgroundColor(Color.GREEN);
                Log.i(TAG, "Green Selected");
                break;
            case R.id.radioButtonBlue:
                textViewColour.setText(R.string.colour_blue);
                textViewColour.setBackgroundColor(Color.BLUE);
                Log.i(TAG, "Blue Selected");
                break;
        }
    }

    public void onCheckBoxClick(View view) {
        if(checkBoxBold.isChecked()) {
            textViewColour.setTypeface(Typeface.DEFAULT_BOLD);
        } else {
            textViewColour.setTypeface(Typeface.DEFAULT);
        }
    }
}