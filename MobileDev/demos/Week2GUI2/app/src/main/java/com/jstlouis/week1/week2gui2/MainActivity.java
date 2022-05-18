package com.jstlouis.week1.week2gui2;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.RatingBar;
import android.widget.Spinner;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity implements AdapterView.OnItemSelectedListener {
    // Tag for logcat
    private final String TAG = "MainActivityDemo";

    // Define screen widgets
    private Spinner spinner;
    private RatingBar ratingBar;
    private EditText editText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialize widgets
        spinner = findViewById(R.id.spinner);
        ratingBar = findViewById(R.id.ratingsBar);
        editText = findViewById(R.id.editText);

        // Spinner Listener Method #1
        //        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
        //            @Override
        //            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        //
        //            }
        //
        //            @Override
        //            public void onNothingSelected(AdapterView<?> adapterView) {
        //
        //            }
        //        });

        // Spinner Listener Method #2
        spinner.setOnItemSelectedListener(this);

        // RatingBar Listener
        ratingBar.setOnRatingBarChangeListener(new RatingBar.OnRatingBarChangeListener() {
            @Override
            public void onRatingChanged(RatingBar ratingBar, float rating, boolean fromUser) {
                Integer i = (int) rating;
                Toast.makeText(getApplicationContext(), i.toString(), Toast.LENGTH_SHORT).show();
                spinner.setSelection(i - 1);
            }
        });

        // TextChanged Listener
        editText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                Log.i(TAG, "beforeChanged");
            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                Log.i(TAG, "onChanged");
            }

            @Override
            public void afterTextChanged(Editable editable) {
                Log.i(TAG, "afterChanged");
            }
        });
    }

    // Spinner Listener Method #2
    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        Toast.makeText(getApplicationContext(), spinner.getSelectedItem().toString(), Toast.LENGTH_LONG).show();
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }
}