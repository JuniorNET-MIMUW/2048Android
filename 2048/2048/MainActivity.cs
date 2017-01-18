using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android2048.Models;
using System.Collections.Generic;

namespace Android2048
{
    public class OnTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        private readonly Action<MotionEvent> _toCall;

        public OnTouchListener(Action<MotionEvent> action)
        {
            _toCall = action;
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            _toCall(e);
            return true;
        }
    }


    [Activity(Label = "2048", MainLauncher = true, Icon = "@drawable/icon", 
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity, GestureDetector.IOnGestureListener
    {
        private DirectionEnum GetDirection(double dx, double dy)
        {
            //TODO
        }

        private void SwipeTo(DirectionEnum direction)
        {
            //TODO

            _freeButtons = new List<Button>();
            foreach (var button in _buttons)
            {
                if(string.IsNullOrEmpty(button.Text))
                    _freeButtons.Add(button);
            }

            //TODO losowanie
        }

        private GestureDetector _detector;


        private List<Button> _freeButtons = new List<Button>();
        private Button[,] _buttons;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            _buttons = new[,]
            {
                {
                    FindViewById<Button>(Resource.Id.button11),
                    FindViewById<Button>(Resource.Id.button12),
                    FindViewById<Button>(Resource.Id.button13),
                    FindViewById<Button>(Resource.Id.button14)
                },
                {
                    FindViewById<Button>(Resource.Id.button21),
                    FindViewById<Button>(Resource.Id.button22),
                    FindViewById<Button>(Resource.Id.button23),
                    FindViewById<Button>(Resource.Id.button24)
                },
                {
                    FindViewById<Button>(Resource.Id.button31),
                    FindViewById<Button>(Resource.Id.button32),
                    FindViewById<Button>(Resource.Id.button33),
                    FindViewById<Button>(Resource.Id.button34)
                },
                {
                    FindViewById<Button>(Resource.Id.button41),
                    FindViewById<Button>(Resource.Id.button42),
                    FindViewById<Button>(Resource.Id.button43),
                    FindViewById<Button>(Resource.Id.button44)
                }
            };

            _detector = new GestureDetector(this);

            foreach (var button in _buttons)
            {
                _freeButtons.Add(button);
                button.SetOnTouchListener(new OnTouchListener(motionEvent =>
                {
                    _detector.OnTouchEvent(motionEvent);
                }));
            }

            Random random = new Random();
            _buttons[random.Next(3), random.Next(3)].Text = "2";
            _buttons[random.Next(3), random.Next(3)].Text = "2";
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            _detector.OnTouchEvent(e);
            return true;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            double dx = e2.GetX() - e1.GetX();
            double dy = e2.GetY() - e1.GetY();

            var dir = GetDirection(dx, dy);
            SwipeTo(dir);

            Toast.MakeText(this, $"Velocities: {velocityX}, {velocityY}", ToastLength.Short).Show();

            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }

        public void OnShowPress(MotionEvent e)
        {
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }
}

