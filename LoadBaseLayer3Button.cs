using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BaseLayerLoader
{
    internal class LoadBaseLayer3Button : Button
    {
        protected override void OnClick()
        {
            string layerPath = Properties.Settings.Default.BaseLayer3;

            if (string.IsNullOrWhiteSpace(layerPath))
            {
                MessageBox.Show("No path set for Base Layer 1. Please save a path first.");
                return;
            }

            // Run on QueuedTask to access ArcGIS Pro map
            QueuedTask.Run(() =>
            {
                try
                {
                    var map = MapView.Active?.Map;
                    if (map == null)
                    {
                        MessageBox.Show("No active map found.");
                        return;
                    }

                    // If it’s a local layer file or feature class
                    if (File.Exists(layerPath))
                    {
                        LayerFactory.Instance.CreateLayer(new Uri(layerPath), map);
                    }
                    else if (Uri.IsWellFormedUriString(layerPath, UriKind.Absolute))
                    {
                        // Assume it's a web layer/service
                        LayerFactory.Instance.CreateLayer(new Uri(layerPath), map);
                    }
                    else
                    {
                        MessageBox.Show("Invalid path or URL: " + layerPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading layer: " + ex.Message);
                }
            });
        }
    }
}
