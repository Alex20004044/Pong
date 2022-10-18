using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
/*using ICSharpCode.NRefactory.Ast;*/
using UnityEngine.Rendering;
using UnityEditor.UIElements;

namespace MSFD.SpawnSystem
{
    public class SpawnHubDataEditorWindow : ExtendedEditorWindow
    {
        SpawnHubData spawnHubData;

        SpawnHubDisplayMode spawnHubDisplayMode = SpawnHubDisplayMode.main;
        int currentIndividualSpawnListIndex = 0;
        SerializedProperty spawnLists;

        Vector2 sidebarScrollViewPos = new Vector2();
        Vector2 mainWindowScrollViewPos = new Vector2();
        List<bool> isSpawnListCurvesOpen = new List<bool>();
        List<bool> isWavesMetricsOpen = new List<bool>();
        //Inrerface parametres
        public GUIStyle displayModeButtonsGUIStyle;
        float sidebarMaxWidth = 150;

        float unitFieldWidth = 140;
        float waveColumnWidth = 40f;
        float defaultDelayBeforeNextWave = 5f;


        float addUnitButtonWidth = 20f;
        string unitsLabelName = "Units";

        float addWaveButtonWidth = 120;
        string addWavebuttonName = "Add wave";
        string removeWavebuttonName = "Remove wave";
        string wavesCountName = "Waves Count: ";

        string waveDelayName = "Waves Delay: ";
        string nextWaveConditionName = "Next wave Condition: ";

        string spawnerFieldName = "Spawner";

        float spaceBetweenSpawnLists = 30f;

        //Interface labels
        string mainModeButtonName = "Main Mode";
        string settingsModeButtonName = "Settings Mode";

        float totalMetricWidth = 200f;
        string totalWeightName = "Total Weight: ";
        string totalTimeName = "Total Time: ";
        string totalPowerName = "Total Power: ";

        //float spawnListMetricWidth = 120f;
        string spawnListWeightName = "SpawnList Weight: ";
        string spawnListTimeName = "SpawnList Time: ";
        string spawnListPowerName = "SpawnList Power: ";

        string waveWeightMetricsName = "Wave Weight: ";
        string waveTimeMetricsName = "Wave Time: ";
        string wavePowerMetricsName = "Wave Power: ";
        string waveMetricsFoldoutName = "WaveMetrics";

        float curveWidth = 200f;

        string spawnListWeightCoefficientName = "Weight coefficient: ";
        string spawnListTotalTimeInfluenceName = "TotalTimeInfluence: ";

        float defaultFloatFieldWidth = 40f;
        float defaultStringFieldWidth = 120f;
        float defaultEnumFieldWidth = 80f;

        float defaultSmallSpace = 20f;
        float defaultNormalSpace = 80f;

        float defaultMaxWidthColumn = 200f;
        float defaultHardcodeValueAntiBug = 3f;
        string settingsColumnWidthName = "ColumnWidth";
        public static void Open(SpawnHubData spawnHubData)
        {
            SpawnHubDataEditorWindow window = GetWindow<SpawnHubDataEditorWindow>("Spawn Hub");
            window.serializedObject = new SerializedObject(spawnHubData);
        }

        private void OnGUI()
        {
            //Initialization
            spawnHubData = serializedObject.targetObject as SpawnHubData;
            spawnLists = serializedObject.FindProperty("spawnLists");

            int delta = spawnHubData.spawnLists.Count - isSpawnListCurvesOpen.Count;
            if (delta > 0)
            {
                for (int i = 0; i < delta; i++)
                {
                    isSpawnListCurvesOpen.Add(false);
                    isWavesMetricsOpen.Add(false);
                }
            }
            else if(delta < 0)
            {
                for (int i = 0; i < -delta; i++)
                {
                    isSpawnListCurvesOpen.RemoveAt(isSpawnListCurvesOpen.Count - 1);
                    isWavesMetricsOpen.RemoveAt(isWavesMetricsOpen.Count - 1);
                }
            }

            //
            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(sidebarMaxWidth), GUILayout.ExpandHeight(true));
            sidebarScrollViewPos = EditorGUILayout.BeginScrollView(sidebarScrollViewPos, GUILayout.Width(sidebarMaxWidth));
            DrawSpawnHubSidebar();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            mainWindowScrollViewPos = EditorGUILayout.BeginScrollView(mainWindowScrollViewPos, GUILayout.ExpandWidth(true));
            DrawSelectedMode();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }
        void DrawSpawnHubSidebar()
        {

            //draw main button
            if (GUILayout.Button(mainModeButtonName))
            {
                spawnHubDisplayMode = SpawnHubDisplayMode.main;
                currentIndividualSpawnListIndex = -1;
            }
            int i = 0;
            foreach (SerializedProperty p in spawnLists)
            {
                string buttonName;
                if (p.objectReferenceValue == null)
                {
                    buttonName = p.displayName;
                }
                else
                {
                    buttonName = p.objectReferenceValue.name;
                }
                buttonName = i + ") " + buttonName;
                if (GUILayout.Button(buttonName, GUILayout.MaxWidth(sidebarMaxWidth)))
                {
                    spawnHubDisplayMode = SpawnHubDisplayMode.individualSpawnList;
                    currentIndividualSpawnListIndex = i;
                }
                i++;
            }
            if (GUILayout.Button(settingsModeButtonName))
            {
                spawnHubDisplayMode = SpawnHubDisplayMode.settings;
                currentIndividualSpawnListIndex = -1;
            }
        }
        void DrawSelectedMode()
        {
            switch (spawnHubDisplayMode)
            {
                case SpawnHubDisplayMode.main:
                    {
                        EditorGUILayout.LabelField("Main mode");
                        DrawMainMode();
                        break;
                    }
                case SpawnHubDisplayMode.individualSpawnList:
                    {
                        EditorGUILayout.LabelField("Display " + currentIndividualSpawnListIndex + " element");
                        DrawSpawnList(currentIndividualSpawnListIndex);
                        break;
                    }
                case SpawnHubDisplayMode.settings:
                    {
                        EditorGUILayout.LabelField("Settings mode");
                        DrawSettingsMode();                 
                        break;
                    }
                default:
                    {
                        EditorGUILayout.LabelField("Something went wrong try to restart window");
                        break;
                    }
            }
            
        }

        void DrawMainMode()
        {
            //EditorGUILayout.BeginScrollView
            //Draw common info and manipulate buttons
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button(addWavebuttonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnHubData.WavesCount++;
                //refresh
                return;
            }
            if (GUILayout.Button(removeWavebuttonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnHubData.WavesCount--;
                if(spawnHubData.WavesCount < 0)
                {
                    spawnHubData.WavesCount = 0;
                }
                //refresh
                return;
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(wavesCountName, GUILayout.Width(addWaveButtonWidth / 2));
            spawnHubData.WavesCount = EditorGUILayout.IntField(spawnHubData.WavesCount, GUILayout.Width(addWaveButtonWidth / 2));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            //Draw total metrics
            //EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(10));
            EditorGUILayout.BeginHorizontal();// GUILayout.MaxWidth(defaultMaxWidthColumn));
            DrawMetrics(totalWeightName, spawnHubData.GetTotalWeight(), totalMetricWidth, spawnHubData.WavesCount, spawnHubData.GetWeightInWave, curveWidth);
            DrawMetrics(totalTimeName, spawnHubData.GetTotalTime(), totalMetricWidth, spawnHubData.WavesCount, spawnHubData.GetTimeInWave, curveWidth);
            DrawMetrics(totalPowerName, spawnHubData.GetTotalPower(), totalMetricWidth, spawnHubData.WavesCount, spawnHubData.GetPowerInWave, curveWidth);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndHorizontal();

            

            //Draw SpawnLists
            EditorGUILayout.BeginVertical();

            //Draw delay before waves
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(defaultMaxWidthColumn));
            EditorGUILayout.BeginVertical();
            //EditorGUILayout.ObjectField("Waves delay: ", null, typeof(SpawnHubDataEditorWindow), GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(waveDelayName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.LabelField(nextWaveConditionName, GUILayout.Width(unitFieldWidth));
            EditorGUILayout.EndVertical();

            for (int i = 0; i < spawnHubData.delayBeforeNextWave.Count; i++)
            {
                EditorGUILayout.BeginVertical();
                //Lutiy Hardcode. Chotbi verstca ne ehala!!!!!!!!! + 3
                spawnHubData.delayBeforeNextWave[i] = EditorGUILayout.FloatField(spawnHubData.delayBeforeNextWave[i], GUILayout.Width(waveColumnWidth + defaultHardcodeValueAntiBug));
                spawnHubData.nextWaveConditions[i] = (SpawnHubData.SHDNextWaveCondition) EditorGUILayout.EnumPopup(spawnHubData.nextWaveConditions[i], GUILayout.Width(waveColumnWidth + defaultHardcodeValueAntiBug));
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(defaultSmallSpace);

            for (int i = 0; i < spawnHubData.spawnLists.Count; i++)
            {
                DrawSpawnList(i);
                EditorGUILayout.Space(spaceBetweenSpawnLists);
            }
            EditorGUILayout.EndVertical();

            
        }

        void DrawMetrics(string labelName, float value, float labelWidth, int numSteps, Func<int, float> curveFunction, float curveWidth)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(labelName + value, GUILayout.Width(labelWidth));
            DrawCurve(numSteps, curveFunction, curveWidth);
            EditorGUILayout.EndVertical();
        }
        void DrawCurve(int numSteps, Func<int, float> curveFunction, float width)
        {
            AnimationCurve animationCurve = new AnimationCurve();
            for (int i = 0; i < numSteps; i++)
            {
                Keyframe keyframe = new Keyframe(i + 1, curveFunction(i));
                animationCurve.AddKey(keyframe);
            }
            EditorGUILayout.CurveField(animationCurve, GUILayout.Width(width), GUILayout.Height(width/2));
        }
        void DrawSpawnList(int spawnListIndex)
        {
            SpawnListBase spawnList = spawnHubData.spawnLists[spawnListIndex];

            //
            //Check spawn Lists for correctness
            //
            //Add attention window!!!!!
            int deltaWaves = spawnHubData.WavesCount - spawnList.GetWavesCount();
            if (deltaWaves > 0)
            {
                for (int i = 0; i < deltaWaves; i++)
                {
                    spawnList.AddWave();
                }
            }
            //Attention window
            else if(deltaWaves < 0)
            {
                for (int i = 0; i < -deltaWaves; i++)
                {
                    spawnList.RemoveWave();
                }
            }


            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(spawnList.name, GUILayout.Width(defaultStringFieldWidth));
            EditorGUILayout.LabelField(spawnListWeightCoefficientName, GUILayout.Width(defaultStringFieldWidth));
            spawnList.spawnListWeightCoefficient = EditorGUILayout.FloatField(spawnList.spawnListWeightCoefficient, GUILayout.Width(defaultFloatFieldWidth));

            EditorGUILayout.LabelField(spawnListTotalTimeInfluenceName, GUILayout.Width(defaultStringFieldWidth));
            spawnList.workMode = (WorkModeSpawnList) 
                EditorGUILayout.EnumPopup(spawnList.workMode, GUILayout.Width(defaultEnumFieldWidth));

            EditorGUILayout.Space(defaultSmallSpace, false);
            EditorGUILayout.LabelField(spawnerFieldName, GUILayout.Width(defaultStringFieldWidth));
            spawnList.SetSpawnerPrefab((SpawnerBase)EditorGUILayout.ObjectField("", spawnList.GetSpawnerType(), typeof(SpawnerBase), false, GUILayout.Width(unitFieldWidth)));
            EditorGUILayout.EndHorizontal();

            //Common render
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(defaultMaxWidthColumn));// GUILayout.MinWidth(defaultMaxWidthColumn), GUILayout.MaxWidth(defaultMaxWidthColumn));           
            DrawUnitData(spawnList, spawnListIndex);
            DrawWavesData(spawnList, spawnListIndex, isWavesMetricsOpen[spawnListIndex]);               
            EditorGUILayout.EndHorizontal();
            //

            EditorGUILayout.BeginVertical();


            DrawSpawnListSettings(spawnList);
            DrawSpawnListCurves(spawnListIndex, spawnList);
            EditorGUILayout.EndVertical();

            //Save SpawnList Changes
            spawnLists.GetArrayElementAtIndex(spawnListIndex).serializedObject.ApplyModifiedProperties();
        }
        void DrawUnitData(SpawnListBase spawnList, int spawnListIndex)
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField( unitsLabelName, GUILayout.Width(unitFieldWidth - addUnitButtonWidth * 4f));
            if (GUILayout.Button("+", GUILayout.Width(addUnitButtonWidth)))
            {
                AddUnitFieldToSpawnList(spawnList);
            }
            if(GUILayout.Button("-", GUILayout.Width(addUnitButtonWidth)))
            {
                RemoveUnitFieldToSpawnList(spawnList);
            }

            EditorGUILayout.EndHorizontal();

            List<UnitDataBase> units = spawnList.units;
            for (int i =0; i < units.Count; i++)
            {
                spawnList.DrawUnitField(i, unitFieldWidth);
            }
            EditorGUILayout.Space(defaultSmallSpace);
            spawnList.DrawUnderUnitField(unitFieldWidth);
            DrawWavesMetricsLabels(spawnList, spawnListIndex);

            EditorGUILayout.EndVertical();
        }
        void AddUnitFieldToSpawnList(SpawnListBase spawnList)
        {
            spawnList.AddUnitDataField();
        }
        void RemoveUnitFieldToSpawnList(SpawnListBase spawnList)
        {
            spawnList.RemoveUnitDataField();
        }

        void DrawWavesData(SpawnListBase spawnList, int spawnListIndex, bool isDrawWavesMetrics)
        {
            EditorGUILayout.BeginHorizontal();
           // EditorGUILayout.LabelField("WAVES", GUILayout.Width(unitFieldWidth));

            for(int i = 0; i < spawnHubData.WavesCount; i++)
            {
                DrawWavesColumn(i, spawnList, isDrawWavesMetrics);
            }

            EditorGUILayout.EndHorizontal();

        }
        void DrawWavesColumn(int waveInd, SpawnListBase spawnList, bool isDrawWavesMetrics)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField((waveInd + 1).ToString(), GUILayout.Width(waveColumnWidth));
            
            spawnList.DrawWaveColumn(waveInd, waveColumnWidth);
            EditorGUILayout.Space(defaultSmallSpace);
            spawnList.DrawUnderWaveColumn(waveInd, waveColumnWidth);
            DrawWavesMetrics(waveInd, spawnList, isDrawWavesMetrics);

            EditorGUILayout.EndVertical();
        }
        void DrawWavesMetricsLabels(SpawnListBase spawnList, int spawnListIndex)
        {
            isWavesMetricsOpen[spawnListIndex] = EditorGUILayout.Foldout(isWavesMetricsOpen[spawnListIndex], waveMetricsFoldoutName);
            if (isWavesMetricsOpen[spawnListIndex])
            {
                EditorGUILayout.LabelField(waveWeightMetricsName, GUILayout.Width(unitFieldWidth));
                EditorGUILayout.LabelField(waveTimeMetricsName, GUILayout.Width(unitFieldWidth));
                EditorGUILayout.LabelField(wavePowerMetricsName, GUILayout.Width(unitFieldWidth));
            }
        }
        /// <summary>
        /// Attention! You should use correct width (гтше ашуды цшвер) foк labels in this method
        /// </summary>
        /// <param name="spawnList"></param>
        void DrawWavesMetrics(int waveInd, SpawnListBase spawnList, bool isDrawWavesMetrics)
        {
            EditorGUILayout.LabelField("", GUILayout.Width(waveColumnWidth));
            if (isDrawWavesMetrics)
            {
                EditorGUILayout.LabelField(spawnList.GetWeightInWave(waveInd).ToString(), GUILayout.Width(waveColumnWidth));
                EditorGUILayout.LabelField(spawnList.GetTimeInWave(waveInd).ToString(), GUILayout.Width(waveColumnWidth));

                float wavePower = spawnList.GetPowerInWave(waveInd);
                string wavePowerString = (float.IsNaN(wavePower) || float.IsInfinity(wavePower)) ? "∞" : wavePower.ToString();
                EditorGUILayout.LabelField(wavePowerString.ToString(), GUILayout.Width(waveColumnWidth));
            }
        }

        void DrawSpawnListSettings(SpawnListBase spawnList)
        {

        }
        void DrawSpawnListCurves(int spawnListIndex, SpawnListBase spawnList)
        {
            isSpawnListCurvesOpen[spawnListIndex] = EditorGUILayout.Foldout(isSpawnListCurvesOpen[spawnListIndex], "Metrics");

            if (isSpawnListCurvesOpen[spawnListIndex])
            {
                EditorGUILayout.BeginHorizontal();
                DrawMetrics(spawnListWeightName, spawnList.GetTotalWeight(), totalMetricWidth, spawnHubData.WavesCount, spawnList.GetWeightInWave, curveWidth);
                DrawMetrics(spawnListTimeName, spawnList.GetTotalTime(), totalMetricWidth, spawnHubData.WavesCount, spawnList.GetTimeInWave, curveWidth);
                DrawMetrics(spawnListPowerName, spawnList.GetTotalPower(), totalMetricWidth, spawnHubData.WavesCount, spawnList.GetPowerInWave, curveWidth);
                EditorGUILayout.EndHorizontal();
            }
        }


        void DrawSettingsMode()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(settingsColumnWidthName, GUILayout.Width(defaultStringFieldWidth));
            waveColumnWidth = EditorGUILayout.FloatField(waveColumnWidth, GUILayout.Width(defaultFloatFieldWidth));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(defaultNormalSpace);
            BetterPropertyField.DrawSerializedProperty(serializedObject.GetIterator());
        }
        enum SpawnHubDisplayMode {main, individualSpawnList, settings };
    }
    
}
