using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using TronGame;
using System.Windows.Media.Media3D;
using SensorWpf;
using System.Diagnostics;

namespace TronAdventure
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        private WPFTronGame game;

        private const int speed = 100;
        private const int gridSpace = 6;
        private const int gridWidth = 50;
        private const int gridHeight = 50;

        private Trackball _trackBall;

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                Line l = new Line();
                l.Stroke = Brushes.LightGray;
                l.X1 = i * gridSpace;
                l.Y1 = 0;
                l.X2 = i * gridSpace;
                l.Y2 = (gridWidth - 1) * gridSpace;
                canvas.Children.Add(l);
            }
            for (int j = 0; j < gridHeight; j++)
            {
                Line l = new Line();
                l.Stroke = Brushes.LightGray;
                l.Y1 = j * gridSpace;
                l.X1 = 0;
                l.Y2 = j * gridSpace;
                l.X2 = (gridHeight - 1) * gridSpace;
                canvas.Children.Add(l);
            }

            //Floor
            for (int i = 0; i < 1; i++)
            {
                ModelVisual3D model = new ModelVisual3D();
                model.Content = Resources["floorModel"] as Model3D;

                Transform3DGroup group = new Transform3DGroup();
                model.Transform = group;

                ScaleTransform3D scale = new ScaleTransform3D();
                scale.ScaleX = gridWidth * 0.01 * 2;
                scale.ScaleY = gridHeight * 0.01 * 2;
                scale.ScaleZ = 0.01;

                group.Children.Add(scale);
                RotateTransform3D rotate = new RotateTransform3D();
                group.Children.Add(rotate);
                TranslateTransform3D translate = new TranslateTransform3D();
                group.Children.Add(translate);
                translate.OffsetX = (gridWidth * 0.01 - 0.01);
                translate.OffsetY = -(gridHeight * 0.01 - 0.01);
                translate.OffsetZ = -0.03;

                modelVisual3DRoot.Children.Add(model);
            }

            for (int i = 0; i < 1; i++)
            {
                ModelVisual3D model = new ModelVisual3D();
                model.Content = Resources["gridModel"] as Model3D;

                Transform3DGroup group = new Transform3DGroup();
                model.Transform = group;

                ScaleTransform3D scale = new ScaleTransform3D();
                scale.ScaleX = 0.01;
                scale.ScaleY = 0.01;
                scale.ScaleZ = 0.1;

                group.Children.Add(scale);
                RotateTransform3D rotate = new RotateTransform3D();
                group.Children.Add(rotate);
                TranslateTransform3D translate = new TranslateTransform3D();
                group.Children.Add(translate);

                modelVisual3DRoot.Children.Add(model);
            }
            
            //Affichage des colonnes
            for (int i = 0; i < gridWidth; i++)
            {
                ModelVisual3D model = new ModelVisual3D();
                model.Content = Resources["gridModel"] as Model3D;

                Transform3DGroup group = new Transform3DGroup();
                model.Transform = group;

                ScaleTransform3D scale = new ScaleTransform3D();
                scale.ScaleX = 0.01;
                scale.ScaleY = gridHeight * 0.02 - 0.01;
                scale.ScaleZ = 0.001;

                group.Children.Add(scale);
                RotateTransform3D rotate = new RotateTransform3D();
                group.Children.Add(rotate);
                TranslateTransform3D translate = new TranslateTransform3D();
                group.Children.Add(translate);

                translate.OffsetX = i * 0.02;
                translate.OffsetY = (gridHeight) * 0.01 - 0.01;
                translate.OffsetZ = -0.01;

                translate.OffsetY = -translate.OffsetY;

                modelVisual3DRoot.Children.Add(model);
            }

            //Affichage des lignes
            for (int i = 0; i < gridHeight; i++)
            {
                ModelVisual3D model = new ModelVisual3D();
                model.Content = Resources["gridModel"] as Model3D;

                Transform3DGroup group = new Transform3DGroup();
                model.Transform = group;

                ScaleTransform3D scale = new ScaleTransform3D();
                scale.ScaleX = (gridWidth) * 0.02 - 0.01;
                scale.ScaleY = 0.01;
                scale.ScaleZ = 0.001;

                group.Children.Add(scale);
                RotateTransform3D rotate = new RotateTransform3D();
                group.Children.Add(rotate);
                TranslateTransform3D translate = new TranslateTransform3D();
                group.Children.Add(translate);
                translate.OffsetX = (gridWidth) * 0.01 - 0.01;
                translate.OffsetY = i * 0.02;
                translate.OffsetZ = -0.01;

                translate.OffsetY = -translate.OffsetY;

                modelVisual3DRoot.Children.Add(model);
            }

            // init des capteurs Windows 7
            if (WpfSensorManager.Current.Accelerometers3D.Count >= 1)
            {
                WpfSensorManager.Current.Accelerometers3D[0].AccelerationDataUpdated += new WpfAccelerometer3DDataUpdatedDelegate(Window1_AccelerationDataUpdated);

                WpfSensorManager.Current.SwitchArrays[0].SwitchStateChanged += new WpfSwitchArrayDataUpdatedDelegate(Window1_SwitchStateChanged);
                WpfSensorManager.Current.SwitchArrays[1].SwitchStateChanged += new WpfSwitchArrayDataUpdatedDelegate(Window1_SwitchStateChanged);
            }


            ResetCamera(300);
        }

        private void TestClick(object sender, RoutedEventArgs args)
        {
        }

        private void ApplyDoubleAnimation(double to, TimeSpan ts, Animatable source, DependencyProperty dp)
        {
            DoubleAnimation da = new DoubleAnimation(to, new Duration(ts));
            source.ApplyAnimationClock(dp, da.CreateClock());
        }

        private void AddWall(DrawingArgs e)
        {
            ModelVisual3D model = new ModelVisual3D();

            model.Content = Resources["wallModel" + e.PlayerNumber.ToString()] as Model3D;
            
            Transform3DGroup group = new Transform3DGroup();
            model.Transform = group;

            ScaleTransform3D scale = new ScaleTransform3D();
            if (e.From.X == e.To.X)
                scale.ScaleX = 0.01;
            else
                scale.ScaleX = 0.02 * (Math.Abs(e.To.X - e.From.X)) + 0.01;
            if (e.From.Y == e.To.Y)
                scale.ScaleY = 0.01;
            else
                scale.ScaleY = 0.02 * (Math.Abs(e.To.Y - e.From.Y)) + 0.01;
            scale.ScaleZ = 0.02;

            group.Children.Add(scale);
            RotateTransform3D rotate = new RotateTransform3D();
            group.Children.Add(rotate);
            TranslateTransform3D translate = new TranslateTransform3D();
            group.Children.Add(translate);
            double x = (e.To.X + e.From.X) / 2;
            double y = (e.To.Y + e.From.Y) / 2;
            translate.OffsetX = (x) * 0.02;
            translate.OffsetY = (y) * 0.02;

            if (Math.Abs(e.To.X - e.From.X) % 2 == 1)
                translate.OffsetX += 0.01;
            if (Math.Abs(e.To.Y - e.From.Y) % 2 == 1)
                translate.OffsetY += 0.01;

            translate.OffsetY = -translate.OffsetY;

            modelVisual3DRoot.Children.Add(model);

            //Camera animation

            if (e.PlayerNumber == 0)
            {
                AxisAngleRotation3D xRotation
                    = ((camera.Transform as Transform3DGroup).Children[0] as RotateTransform3D).Rotation as AxisAngleRotation3D;
                AxisAngleRotation3D rotation
                    = ((camera.Transform as Transform3DGroup).Children[1] as RotateTransform3D).Rotation as AxisAngleRotation3D;
                TranslateTransform3D translation
                    = (camera.Transform as Transform3DGroup).Children[2] as TranslateTransform3D;

                double offsetX = e.From.X * 0.02;
                double offsetY = e.From.Y * 0.02;

                double recul = 0.2;

                if (e.To.X != e.From.X)
                {
                    if (e.From.X < e.To.X)
                        offsetX -= recul;
                    else
                        offsetX += recul;
                }
                if (e.To.Y != e.From.Y)
                {
                    if (e.From.Y < e.To.Y)
                        offsetY -= recul;
                    else
                        offsetY += recul;
                }

                offsetY = -offsetY;

                ApplyDoubleAnimation(offsetX, new TimeSpan(0, 0, 0, 0, 300), translation, TranslateTransform3D.OffsetXProperty);
                ApplyDoubleAnimation(offsetY, new TimeSpan(0, 0, 0, 0, 300), translation, TranslateTransform3D.OffsetYProperty);
                ApplyDoubleAnimation(0.08, new TimeSpan(0, 0, 0, 0, 300), translation, TranslateTransform3D.OffsetZProperty);

                //camera.LookDirection = new Vector3D(-translate.OffsetX, -translate.OffsetY, 0);

                double angle = 0;

                switch (e.Direction)
                {
                    case Direction.Down:
                        angle = 180;
                        break;
                    case Direction.Left:
                        angle = 90;
                        break;
                    case Direction.Right:
                        angle = 270;
                        break;
                    case Direction.Up:
                        angle = 0;
                        break;
                }

                double animatedAngle = rotation.Angle;

                double tmp = Math.Floor(animatedAngle / 360);
                angle = angle + (tmp * 360);
                if (angle > animatedAngle)
                {
                    if (Math.Abs(animatedAngle - angle) > 180)
                        angle -= 360;
                }
                else
                {
                    if (Math.Abs(animatedAngle - angle) > 180)
                        angle += 360;
                }

                ApplyDoubleAnimation(-10, new TimeSpan(0, 0, 0, 0, 300), xRotation, AxisAngleRotation3D.AngleProperty);
                ApplyDoubleAnimation(angle, new TimeSpan(0, 0, 0, 0, 300), rotation, AxisAngleRotation3D.AngleProperty);
                //DoubleAnimation animRotation =
                //    new DoubleAnimation(angle, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
                //rotation.ApplyAnimationClock(AxisAngleRotation3D.AngleProperty, animRotation.CreateClock());
            }
        }

        private void ResetCamera(int ms)
        {
            AxisAngleRotation3D xRotation
                = ((camera.Transform as Transform3DGroup).Children[0] as RotateTransform3D).Rotation as AxisAngleRotation3D;
            AxisAngleRotation3D rotation
                = ((camera.Transform as Transform3DGroup).Children[1] as RotateTransform3D).Rotation as AxisAngleRotation3D;
            TranslateTransform3D translation
                = (camera.Transform as Transform3DGroup).Children[2] as TranslateTransform3D;

            double offsetX = 0.5;
            double offsetY = -0.5;
            double offsetZ = 1;

            ApplyDoubleAnimation(offsetX, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetXProperty);
            ApplyDoubleAnimation(offsetY, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetYProperty);
            ApplyDoubleAnimation(offsetZ, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetZProperty);

            double angle = 0;

            ApplyDoubleAnimation(-90, new TimeSpan(0, 0, 0, 0, ms), xRotation, AxisAngleRotation3D.AngleProperty);
            ApplyDoubleAnimation(angle, new TimeSpan(0, 0, 0, 0, ms), rotation, AxisAngleRotation3D.AngleProperty);
        }

        private void Intro(int ms)
        {
            AxisAngleRotation3D xRotation
                = ((camera.Transform as Transform3DGroup).Children[0] as RotateTransform3D).Rotation as AxisAngleRotation3D;
            AxisAngleRotation3D rotation
                = ((camera.Transform as Transform3DGroup).Children[1] as RotateTransform3D).Rotation as AxisAngleRotation3D;
            TranslateTransform3D translation
                = (camera.Transform as Transform3DGroup).Children[2] as TranslateTransform3D;

            double offsetX = 0.5;
            double offsetY = -0.5;
            double offsetZ = 1;

            ApplyDoubleAnimation(offsetX, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetXProperty);
            ApplyDoubleAnimation(offsetY, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetYProperty);
            ApplyDoubleAnimation(offsetZ, new TimeSpan(0, 0, 0, 0, ms), translation, TranslateTransform3D.OffsetZProperty);

            double angle = 0;

            ApplyDoubleAnimation(-90, new TimeSpan(0, 0, 0, 0, ms), xRotation, AxisAngleRotation3D.AngleProperty);
            ApplyDoubleAnimation(angle, new TimeSpan(0, 0, 0, 0, ms), rotation, AxisAngleRotation3D.AngleProperty);
        }

        private void StartGameClick(object sender, RoutedEventArgs args)
        {
            game = new WPFTronGame(1, gridWidth, gridHeight, speed);
            game.NewDraw += new EventHandler<DrawingArgs>(Game_NewDraw);
            game.EndOfGame += new EventHandler<EndOfGameArgs>(Game_EndOfGame);

            game.Start();
        }

        void Game_EndOfGame(object sender, EndOfGameArgs e)
        {
            ResetCamera(1000);
            //MessageBox.Show("Perdu !");
        }

        void Game_NewDraw(object sender, DrawingArgs e)
        {
            Add2DWall(e);

            //Animate 2D Viewport

            double angle = 0;

            switch (e.Direction)
            {
                case Direction.Down:
                    angle = 180;
                    break;
                case Direction.Left:
                    angle = 90;
                    break;
                case Direction.Right:
                    angle = 270;
                    break;
                case Direction.Up:
                    angle = 0;
                    break;
            }

            double animatedAngle = canvasRotation.Angle;

            double tmp = Math.Floor(animatedAngle / 360);
            angle = angle + (tmp * 360);
            if (angle > animatedAngle)
            {
                if (Math.Abs(animatedAngle - angle) > 180)
                    angle -= 360;
            }
            else
            {
                if (Math.Abs(animatedAngle - angle) > 180)
                    angle += 360;
            }

            DoubleAnimation animRotation =
                new DoubleAnimation(angle, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            canvasRotation.ApplyAnimationClock(RotateTransform.AngleProperty, animRotation.CreateClock());

            //3D display

            AddWall(e);
        }

        void Add2DWall(DrawingArgs e)
        {
            Line l = new Line();

            l.StrokeThickness = 2;

            switch (e.PlayerNumber)
            {
                case 0 :
                    l.Stroke = Brushes.BlueViolet;
                    break;
                case 1:
                    l.Stroke = Brushes.Navy;
                    break;
                case 2:
                    l.Stroke = Brushes.BlueViolet;
                    break;
                case 3:
                    l.Stroke = Brushes.BlueViolet;
                    break;
            }

            l.X1 = e.From.X * gridSpace;
            l.Y1 = e.From.Y * gridSpace;

            //l.X2 = e.To.X * gridWidth;
            //l.Y2 = e.To.Y * gridWidth;

            DoubleAnimation animX = 
                new DoubleAnimation((e.From.X) * gridSpace, (e.To.X) * gridSpace, new Duration(new TimeSpan(0, 0, 0, 0, speed)));
            l.ApplyAnimationClock(Line.X2Property, animX.CreateClock());
            DoubleAnimation animY = 
                new DoubleAnimation((e.From.Y) * gridSpace, (e.To.Y) * gridSpace, new Duration(new TimeSpan(0, 0, 0, 0, speed)));
            l.ApplyAnimationClock(Line.Y2Property, animY.CreateClock());

            canvas.Children.Add(l);
        }

        void OnKeyDown(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Right :
                    game.Turn(0, ChangeDirection.Right);
                    break;
                case Key.Left:
                    game.Turn(0, ChangeDirection.Left);
                    break;
                case Key.A:
                    game.Turn(1, ChangeDirection.Left);
                    break;
                case Key.Z:
                    game.Turn(1, ChangeDirection.Right);
                    break;
            }
        }

        // trigger rank that sensor must go back to allow new change direction
        double triggerXMin = 0.0;   
        double triggerXMax = 0.0;
        
        /// <summary>
        /// Event for accelement3D movement management
        /// right/left : turn the "Tron speed"
        /// back/front : increase/decrease "Tron car" speed
        /// </summary>
        /// <param name="sensor">WPFAcceleromet source</param>
        /// <param name="newX">value for the X Axys (turn)</param>
        /// <param name="newY">value for the Y axys (speed)</param>
        /// <param name="newZ">value for the Z Axys (unused)</param>
        void Window1_AccelerationDataUpdated(WpfAccelerometer3D sensor, double newX, double newY, double newZ)
        {
            #region Gestion de la direction : bascule gauche/droite
            if (newX < triggerXMin)
                return;
            if (newX > triggerXMax)
                return;
            triggerXMin = -9999;
            triggerXMax = 9999;
            if (newX < -0.55) // bascule du capteur a gauche
            {
                game.Turn(0, ChangeDirection.Left);
                triggerXMin = -0.3;
                triggerXMax = 9999;
            }
            else if(newX>0.55) // bascule du capteur a droite
            {
                game.Turn(0, ChangeDirection.Right);
                triggerXMax = 0.3;
                triggerXMin = -9999;
            }
            #endregion

            #region gestion de la vitesse : bascule avant/arriere
            // l'inclinaison est limité a 0.4 vers l'avant et -0.6 vers l'arriere
            double yToUsed = newY;
            if (yToUsed > 0.4)
                newY = 0.4;
            else if (yToUsed < -0.6)
                newY = -0.6;
            
            // regle de trois pour tranformer la fourchette d'inclinaison [0.4,-0.6] 
            // en une fourchette de ms pour le timer : [45,180]

            newY = 1 - (newY + 0.6);    // on inverse et decalle en valeur positive [0.0,1.0] 
            Debug.WriteLine(newY);

            int speed = Convert.ToInt32( (180 - 45) * newY + 45);

            if (game != null)
                if (Math.Abs(game.Speed - speed) > 2)   // trigger pour limiter les mouvements dus aux trenblements du joueur :)
                {
                    game.Speed = speed;
                    Dispatcher.Invoke(new Action(() => 
                    { 
                        pgbSpeed.Value = Convert.ToInt32(pgbSpeed.Maximum - newY*pgbSpeed.Maximum);
                    }));
                }

            #endregion


        }


        /// <summary>
        /// Prise en compte de l'activation de switch de la carte
        /// E8=new game
        /// E1/E5=red flash
        /// 
        /// <remarks>Added by Nicolas CLERC for WpfSensor support</remarks>
        /// 
        /// </summary>
        /// <param name="switchArraySensor">sensor de type WpfSwitchArray déclencheur</param>
        /// <param name="switchName">nom du switch</param>
        /// <param name="switchState">true=switch activated/touched, false=switch deactivation/untouched</param>
        void Window1_SwitchStateChanged(WpfSwitchArray switchArraySensor, string switchName, bool switchState)
        {
            if (switchName == "E1" || switchName == "E5")
            {
                if (switchState == true)
                {
                    Dispatcher.Invoke(new Action(
                        delegate()
                        {
                            this.Background = new SolidColorBrush(Colors.Red);
                        }));
                }
                else
                {
                    Dispatcher.Invoke(new Action(
                        delegate()
                        {
                            this.Background = new SolidColorBrush(Colors.White);
                        }));
                }
            }
            else if (switchName == "E8" && switchState==true)
                Dispatcher.Invoke(new Action(
                       delegate()
                       {
                           StartGameClick(this, null);
                       }));
                
        }


    }
}
