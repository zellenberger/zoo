using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Animals;
using CagedItems;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    public partial class CageWindow : Window
    {
        /// <summary>
        /// The animals cage.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">Returns animal.</param>
        public CageWindow(Cage cage)
        {
            this.cage = cage;
            this.InitializeComponent();
            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate()
                    {
                        int zIndex = new int();
                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                this.cageGrid.Children.Remove(v);
                                break;
                            }

                            zIndex++;
                        }

                        if (item.IsActive == true)
                        {
                            this.DrawItem(item, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
        }

        /// <summary>
        /// The draw animal method in the cage window.
        /// </summary>
        /// <param name="item">Returns the item.</param>
        /// <param name="zIndex">Returns the zIndex.</param>
        private void DrawItem(ICageable item, int zIndex)
        {
            string resourceKey = item.ResourceKey;

            Viewbox viewBox = this.GetViewbox(800, 400, item.XPosition, item.YPosition, resourceKey, item.DisplaySize);

            viewBox.HorizontalAlignment = HorizontalAlignment.Left;
            viewBox.VerticalAlignment = VerticalAlignment.Top;

            TransformGroup transformGroup = new TransformGroup();

            if (item.HungerState == HungerState.Tired)
            {
                SkewTransform tiredSkew = new SkewTransform();
                tiredSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;
                transformGroup.Children.Add(tiredSkew);
                transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));
            }

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewBox.
                viewBox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewBox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Apply the ScaleTransform to the viewBox
                transformGroup.Children.Add(flipTransform);
            }

            viewBox.RenderTransform = transformGroup;
            viewBox.Tag = item;
            this.cageGrid.Children.Insert(zIndex, viewBox);
        }

        /// <summary>
        /// Loads the cage window.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        /// <summary>
        /// The draw all animals method in the cage window.
        /// </summary>
        private void DrawAllItems()
        {
            this.cageGrid.Children.Clear();

            int zIndex = 0;

            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex++));
            
            ////foreach (ICageable a in this.cage.CagedItems)
            ////{
            ////    this.DrawItem(a, zIndex++);
            ////}
        }

        /// <summary>
        /// The get view box method in the cage window.
        /// </summary>
        /// <param name="maxXPosition">The max x position.</param>
        /// <param name="maxYPosition">The max y position.</param>
        /// <param name="xPosition">The x position.</param>
        /// <param name="yPosition">The y position.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="displayScale">The display scale.</param>
        /// <returns> Returns finishedViewBox.</returns>
        private Viewbox GetViewbox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewBox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewBox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewBox.
            return finishedViewBox;
        }

        /// <summary>
        /// The window closed method.
        /// </summary>
        /// <param name="sender">Send to next.</param>
        /// <param name="e">Returns multiple.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.cage.OnImageUpdate = null;
        }
    }
}
