using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPR_FORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            double[,] Coordinates = new double[2, 2];

            string[] constraintArr = new string[3];
            constraintArr[0] = "+10+4<=40";
            constraintArr[1] = "+1+1<=7";
            constraintArr[2] = "+0+1>=3";
            //loops through constraints creating the lines
            for (int i = 0; i < constraintArr.Length; i++)
            {
                string constraint = constraintArr[i];
                Coordinates = createConstraintLine(constraint);// generates the points for x1 and x2 and returns as array
                double x1Cord_x1 = Coordinates[0, 0];       // assing points from array to single variables to parse into function that draws Lines on chart
                double x1Cord_x2 = Coordinates[0, 1];       //
                double x2Cord_y1 = Coordinates[1, 0];       //
                double x2Cord_y2 = Coordinates[1, 1];       //
                chartLoad(x1Cord_x1, x1Cord_x2, x2Cord_y1, x2Cord_y2, i + 1); //parse points and counter points used to create Line, counter used to indicate what contraint is currently being work on for example constraint 1 or 2 
            }


            //just testing the feasible region creation 
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series.Add("Fesible region");
            chart1.Series["Fesible region"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Fesible region"].Color = Color.Red;

            chart1.Series["Fesible region"].Points.AddXY(2, 5);
            chart1.Series["Fesible region"].Points.AddXY(0, 3);

            chart1.Series["Fesible region"].Points.AddXY(1, 6);
            chart1.Series["Fesible region"].Points.AddXY(0, 5);


            // finds optimal points will explain in call ALTHOUH  PLEASE NOTE these constraint/if statements as hardcoded currently we need to sub them out dynamic testing for exmaple put into a equation array or something
            bool Optimal = true;
            int Optimalx2 = 0;
            int Optimalx1 = 0;
            for (int i = 10; i >= 0; i--)
            {
                if (10 * (0) + 4 * i <= 40)
                {
                    if (0 + i * 1 <= 7)
                    {
                        if (i * 1 >= 3)
                        {
                            Optimalx2 = i;
                            break;
                        }
                    }
                    else
                    {
                        Optimal = false;

                        continue;
                    }
                }
                else
                {
                    Optimal = false;
                    continue;
                }
            }
            Console.WriteLine(Optimalx2);

            int ConstraintCnt = 0;
            double x1 = 0;
            double x2 = 0;
            double rhs = 0;
            while (ConstraintCnt < constraintArr.Length)
            {
                string equation = constraintArr[ConstraintCnt];
                ConstraintCnt = ConstraintCnt + 1;
                 x1 = 0;
                 x2 = 0;
                string sign = "";
                rhs = 0;
                string x1sign = "";
                string rhsSigns = "";
                x1sign = equation.Substring(0, 1);
                x1sign = x1sign + "1";
                equation = equation.Remove(0, 1);

                string Temp_x1 = "";
                string Temp_x2 = "";
                string Temp_rhs = "";
                string x2sign = "";
                for (int i = 0; i < equation.Length; i++)
                {
                    if (equation[i] == '+' || equation[i] == '-')
                    {
                        x2sign = equation[i].ToString();
                        equation = equation.Remove(0, i + 1);
                        break;
                    }
                    Temp_x1 = Temp_x1 + equation[i];
                    x1 = int.Parse(Temp_x1);
                }

                x1 = x1 * int.Parse(x1sign);
                for (int i = 0; i < equation.Length; i++)
                {
                    if (equation[i] == '+' || equation[i] == '-' || equation[i] == '=' || equation[i] == '>' || equation[i] == '<')
                    {
                        equation = equation.Remove(0, i );
                        break;
                    }
                    Temp_x2 = Temp_x2 + equation[i];
                    x2 = int.Parse(Temp_x2);

                }
                x2sign = x2sign + "1";
                x2 = x2 * int.Parse(x2sign);

                Console.WriteLine(equation);//<=40
                for (int i=0;i<equation.Length;i++)
                {
                    if (equation[i] == '=' || equation[i] == '>' || equation[i] == '<')//check if charcther not >< or = (if it a number )
                    {
                        rhsSigns = rhsSigns + equation[i].ToString(); // if its not a number means its either >,< or = so its adds it the var rhsSigns

                    }
                    else
                    {
                        equation = equation.Remove(0, i); //removes up to that point
                    }

                }
                Temp_rhs = equation;
                rhs = int.Parse(Temp_rhs);
               // Console.WriteLine(rhsSigns);

            }


            for (int i = 7; i >= 0; i--)
            {
                if (x1 * (i) + x2 * (0) <= rhs)
                {
                    if (1 * i + i * 0 <= 7)
                    {
                        Optimalx1 = i;
                        break;
                    }
                    else
                    {
                        Optimal = false;
                        Console.WriteLine(i);
                        continue;
                    }
                }
                else
                {
                    Optimal = false;
                    continue;
                }
            }
            Console.WriteLine(Optimalx1);

            while (Optimalx1 != 0 || Optimalx2 != 0)
            {
                if (10 * (Optimalx1) + 4 * (Optimalx2) <= 40)
                {
                    if (1 * Optimalx1 + Optimalx2 * 1 <= 7)
                    {
                        Console.WriteLine("x1:" + Optimalx1 + " x2:" + Optimalx2);
                        break;
                    }
                    else
                    {
                        Optimalx1 = Optimalx1 - 1;
                        Optimalx2 = Optimalx2 - 1;
                    }
                }
                else
                {
                    Optimalx1 = Optimalx1 - 1;
                    Optimalx2 = Optimalx2 - 1;
                }
            }

            //draws optimal point on chart
            chart1.Series.Add("optimal");
            chart1.Series["optimal"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series["optimal"].Color = Color.Green;

            chart1.Series["optimal"].Points.AddXY(Optimalx1, Optimalx2);

        }
    

        private static double[,] createConstraintLine(string constraint)
        {
            string x1sign = "";
            x1sign = constraint.Substring(0, 1);
            x1sign = x1sign + "1";
            constraint = constraint.Remove(0, 1);
            Console.WriteLine(constraint);

            string Temp_x1 = "";
            string Temp_x2 = "";
            string Temp_rhs = "";
            double x1 = 0;
            double x2 = 0;
            double rhs = 0;
            string x2sign = "";
            for (int i = 0; i < constraint.Length; i++)
            {
                if (constraint[i] == '+' || constraint[i] == '-')
                {
                    x2sign = constraint[i].ToString();
                    constraint = constraint.Remove(0, i + 1);
                    break;
                }
                Temp_x1 = Temp_x1 + constraint[i];
                x1 = int.Parse(Temp_x1);
            }

            x1 = x1 * int.Parse(x1sign);
            for (int i = 0; i < constraint.Length; i++)
            {
                if (constraint[i] == '+' || constraint[i] == '-' || constraint[i] == '=' || constraint[i] == '>' || constraint[i] == '<')
                {
                    constraint = constraint.Remove(0, i + 1);
                    break;
                }
                Temp_x2 = Temp_x2 + constraint[i];
                x2 = int.Parse(Temp_x2);

            }
            x2sign = x2sign + "1";
            x2 = x2 * int.Parse(x2sign);
            for (int i = 0; i < constraint.Length; i++)
            {
                if (constraint[i] == '=' || constraint[i] == '>' || constraint[i] == '<')
                {
                    constraint = constraint.Remove(0, i + 1);

                    break;
                }
            }
            Temp_rhs = Temp_rhs + constraint;
            rhs = int.Parse(Temp_rhs);


            double x1Cord = 0;
            double x2Cord = 0;

            double[,] Coordinates=new double[2,2];

            x1Cord = rhs / x1;
            x2Cord = rhs / x2;

            if (x1 == 0)
            {
                x1Cord = 30;
                Coordinates[0, 0] = x1Cord;
                Coordinates[0, 1] = x2Cord;
                Console.WriteLine(x1Cord + " " + x2Cord);
            }
            else
            {
                Coordinates[0, 0] = x1Cord;
                Coordinates[0, 1] = 0;
            }

            if (x2 == 0)
            {
                x2Cord = 30;
                Coordinates[1, 0] = x1Cord;
                Coordinates[1, 1] = x2Cord;
                Console.WriteLine(x1Cord + " " + x2Cord);
            }
            else
            {
                Coordinates[1, 0] = 0;
                Coordinates[1, 1] = x2Cord;
            }
            return Coordinates;


        }

        private void chartLoad(double x1Cord_x1,double x1Cord_x2 ,double x2Cord_y1, double x2Cord_y2, int const_cnt)
        {
            var chart = chart1.ChartAreas[0];

            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = 0;
            chart.AxisY.Minimum = 0;

            chart.AxisX.Maximum = 30;
            chart.AxisY.Maximum = 30;

            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 1;

            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series.Add("Constraint"+const_cnt);
            chart1.Series["Constraint" + const_cnt].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Constraint" + const_cnt].Color = Color.Blue; // Random color required

            chart1.Series["Constraint" + const_cnt].Points.AddXY(x1Cord_x1, x1Cord_x2);
            chart1.Series["Constraint" + const_cnt].Points.AddXY(x2Cord_y1, x2Cord_y2);

            Console.WriteLine(x1Cord_x1.ToString()+" "+ x1Cord_x2.ToString());

          
            }

        }
    }

