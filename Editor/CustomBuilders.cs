using System;
using System.Collections.Generic;
using UnityEditor;

namespace Toblerone.Toolbox.EditorScripts {
    public static class CustomBuilders {
        public static void BuildWebGL() {
            string[] arguments = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(arguments, "Toblerone.Toolbox.EditorScripts.CustomBuilders.BuildWebGL");
            if (index < 0 || index >= arguments.Length - 1) {
                throw new ArgumentException("[CustomBuilders.BuildWebGL] Could not determine destination path!");
            }
            string destinationPath = arguments[index + 1];
            List<string> scenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                scenes.Add(scene.ToString());
            }
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, destinationPath, BuildTarget.WebGL, BuildOptions.None);
        }

        public static void BuildAndroid() {
            string[] arguments = Environment.GetCommandLineArgs();
            int index = Array.IndexOf(arguments, "Toblerone.Toolbox.EditorScripts.CustomBuilders.BuildAndroid");
            if (index < 0 || index >= arguments.Length - 1) {
                throw new ArgumentException("[CustomBuilders.BuildAndroid] Could not determine destination path!");
            }
            string destinationPath = arguments[index + 1];
            List<string> scenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                scenes.Add(scene.ToString());
            }
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, destinationPath, BuildTarget.Android, BuildOptions.None);
        }
    }
}
