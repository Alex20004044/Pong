using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Rendering;
using UnityEditor.UIElements;

namespace MSFD.PhoenixSpawnSystem
{
    public class PhoenixSpawnListEditorWindow : ExtendedEditorWindow
    {
        SpawnList spawnList;

        Vector2 sidebarScrollViewPos = new Vector2();
        Vector2 mainWindowScrollViewPos = new Vector2();
        List<bool> isSpawnListCurvesOpen = new List<bool>();
        bool isWavesMetricsOpen = false;
        //Inrerface parametres
        public GUIStyle displayModeButtonsGUIStyle;

        GUIStyle centreBoldText;
        public static GUIStyle rightFieldText;

        float sidebarMaxWidth = 150;

        float unitFieldWidth = 140;
        float waveColumnWidth = 40f;
        float defaultDelayBeforeNextWave = 5f;
        float toggleWidth = 20;

        float addUnitButtonWidth = 20f;
        float addUnitButtonHeight = 18;
        string unitsLabelName = "Units";

        

        float addWaveButtonWidth = 120;
        string addWavebuttonName = "Add Wave";
        string removeWavebuttonName = "Remove Wave";
        string addUnitButtonName = "Add Unit";
        string removeUnitButtonName = "Remove Unit";

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
        string waveMetricsFoldoutName = "Metrics";

        float curveWidth = 200f;

        string spawnListWeightCoefficientName = "Weight coefficient: ";
        string spawnListTotalTimeInfluenceName = "TotalTimeInfluence: ";

        float defaultFloatFieldWidth = 40f;
        float defaultStringFieldWidth = 120f;
        float defaultEnumFieldWidth = 80f;

        float defaultSmallSpace = 20f;
        float defaultNormalSpace = 80f;

        float defaultMaxWidthColumn = 200f;
        float defaultMinWidthColumn = 30;
        float defaultHardcodeValueAntiBug = 3f;
        string settingsColumnWidthName = "ColumnWidth";
        float settingsWidth = 400f;

        string unitsCoefficientName = "Coeff";
        string unitsDescriptionName = "Decription";

        float isNeedSpaceWidth = 50f;
        string isNeedSpaceName = "";//@"\space/";

        bool isSettingsOpen = false;

        public static void Open(SpawnList spawnList)
        {
            PhoenixSpawnListEditorWindow window = GetWindow<PhoenixSpawnListEditorWindow>("Spawn List");
            window.serializedObject = new SerializedObject(spawnList);
        }


        private void OnGUI()
        {

            centreBoldText = new GUIStyle(GUI.skin.label);
            centreBoldText.alignment = TextAnchor.MiddleCenter;
            centreBoldText.fontStyle = FontStyle.Bold;
            centreBoldText.fontSize = 13;

            rightFieldText = new GUIStyle(GUI.skin.textField);
            rightFieldText.alignment = TextAnchor.MiddleRight;

            //waveColumnWidth = spawnList.colunmWidth;

            //Initialization
            spawnList = serializedObject.targetObject as SpawnList;

            waveColumnWidth = spawnList.columnWidth;

            DrawMainMode();
            mainWindowScrollViewPos = EditorGUILayout.BeginScrollView(mainWindowScrollViewPos);//, GUILayout.ExpandWidth(true));
            DrawSpawnList();
            EditorGUILayout.EndScrollView();

            EditorUtility.SetDirty(spawnList);
            EditorUtility.SetDirty(this);
        }
        void DrawMainMode()
        {
            //EditorGUILayout.BeginScrollView]

            //EditorGUILayout.BeginVertical("box");
            //Draw common info and manipulate buttons

            
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(spawnList.name, centreBoldText, GUILayout.Width(defaultStringFieldWidth));
            EditorGUILayout.Space(5);
            spawnList.unitsPerGroupAllocation = (UnitsPerGroupAllocation)UnityEditor.EditorGUILayout.ObjectField("", spawnList.unitsPerGroupAllocation, typeof(UnitsPerGroupAllocation),
            false, GUILayout.Width(unitFieldWidth));

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button(addUnitButtonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnList.AddUnitDataField();
                return;
            }
            if (GUILayout.Button(removeUnitButtonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnList.RemoveUnitDataField();
                return;
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button(addWavebuttonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnList.AddWave();
                return;
            }
            if (GUILayout.Button(removeWavebuttonName, GUILayout.Width(addWaveButtonWidth)))
            {
                spawnList.RemoveWave();
                return;
            }
            EditorGUILayout.EndVertical();
            //Draw total metrics
            //EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(10));



            EditorGUILayout.BeginHorizontal("box", GUILayout.Width(curveWidth * 3));// GUILayout.MaxWidth(defaultMaxWidthColumn));
            DrawMetrics(totalWeightName, spawnList.GetTotalWeight(), totalMetricWidth, spawnList.waves.Count, spawnList.GetWeightInWave, curveWidth);
            DrawMetrics(totalTimeName, spawnList.GetTotalTime(), totalMetricWidth, spawnList.waves.Count, spawnList.GetTimeInWave, curveWidth);
            DrawMetrics(totalPowerName, spawnList.GetTotalPower(), totalMetricWidth, spawnList.waves.Count, spawnList.GetPowerInWave, curveWidth);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(defaultNormalSpace);

            DrawSettings();

            EditorGUILayout.EndHorizontal();


        }
        void DrawSettings()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.Width(settingsWidth));

            isSettingsOpen = EditorGUILayout.Foldout(isSettingsOpen, "System", true);
            
            if(isSettingsOpen)
            {
                
                spawnList.unitsWeightMode = (SpawnList.UnitsWeightMode)EditorGUILayout.EnumPopup(spawnList.unitsWeightMode, GUILayout.Width(settingsWidth));
                EditorGUILayout.BeginHorizontal();
                spawnList.isUseDefaultWaveDelay = EditorGUILayout.Toggle("Use Default Waves Delay", spawnList.isUseDefaultWaveDelay, GUILayout.Width(settingsWidth/2));//, GUILayout.Width(defaultStringFieldWidth));
                if(spawnList.isUseDefaultWaveDelay)
                {
                    spawnList.defaultWavesDelay = EditorGUILayout.FloatField(spawnList.defaultWavesDelay, GUILayout.Width(settingsWidth/2));
                }
                EditorGUILayout.EndHorizontal();

                spawnList.isDisplayUnitDescription = EditorGUILayout.Toggle("Display Units Description", spawnList.isDisplayUnitDescription, GUILayout.Width(settingsWidth));
                spawnList.columnWidth = Mathf.Clamp(EditorGUILayout.FloatField("Column Width: ", spawnList.columnWidth, GUILayout.Width(settingsWidth)), 
                    defaultMinWidthColumn, defaultMaxWidthColumn);
                //waveColumnWidth = EditorGUILayout.FloatField("Column Width: " , waveColumnWidth, GUILayout.Width(settingsWidth));
                EditorGUILayout.BeginVertical("box", GUILayout.Width(settingsWidth));
                spawnList.totalWeightMultiplyer = EditorGUILayout.FloatField("Weight Multiplyer: ",spawnList.totalWeightMultiplyer, GUILayout.Width(settingsWidth));
                spawnList.totalAdditionalWeight = EditorGUILayout.FloatField("Additional Weight: ", spawnList.totalAdditionalWeight, GUILayout.Width(settingsWidth));
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical("box", GUILayout.Width(settingsWidth));
                spawnList.totalTimeMultiplyer = EditorGUILayout.FloatField("Time Multiplyer: ", spawnList.totalTimeMultiplyer, GUILayout.Width(settingsWidth));
                spawnList.totalAdditionaTime = EditorGUILayout.FloatField("Additional Time: ", spawnList.totalAdditionaTime, GUILayout.Width(settingsWidth));
                EditorGUILayout.EndVertical();

            }


            EditorGUILayout.EndVertical();
        }
        void DrawSpawnList()
        {
            //EditorGUILayout.BeginHorizontal();         
            //EditorGUILayout.Space(defaultSmallSpace, false);

            //EditorGUILayout.EndHorizontal();

            //Common render
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(defaultMaxWidthColumn));// GUILayout.MinWidth(defaultMaxWidthColumn), GUILayout.MaxWidth(defaultMaxWidthColumn));           
            DrawUnitData(spawnList);

            EditorGUILayout.Space(defaultSmallSpace);

            DrawWavesData(spawnList);
            EditorGUILayout.EndHorizontal();
        }
        void DrawUnitData(SpawnList spawnList)
        {
            EditorGUILayout.BeginVertical();


            //!!!
            DrawWavesMetricsLabels(spawnList);
            //EditorGUILayout.Space(defaultSmallSpace);

            EditorGUILayout.BeginHorizontal();
            
            EditorGUILayout.LabelField(isNeedSpaceName, GUILayout.Width(toggleWidth));

            EditorGUILayout.LabelField(unitsLabelName, centreBoldText, GUILayout.Width(unitFieldWidth));
            /*if (GUILayout.Button("+", GUILayout.Width(addUnitButtonWidth), GUILayout.Height(addUnitButtonHeight)))
            {
                AddUnitFieldToSpawnList(spawnList);
            }
            if (GUILayout.Button("-", GUILayout.Width(addUnitButtonWidth), GUILayout.Height(addUnitButtonHeight)))
            {
                RemoveUnitFieldToSpawnList(spawnList);
            }*/

            if (spawnList.unitsWeightMode == SpawnList.UnitsWeightMode.multiplyByCoefficient)
            {
                EditorGUILayout.LabelField(unitsCoefficientName, GUILayout.Width(waveColumnWidth));
            }
            if(spawnList.isDisplayUnitDescription)
            {
                EditorGUILayout.LabelField(unitsDescriptionName, GUILayout.Width(defaultStringFieldWidth));
            }

            EditorGUILayout.EndHorizontal();

            List<UnitDataBase> units = spawnList.units;
            for (int i = 0; i < units.Count; i++)
            {

                EditorGUILayout.BeginHorizontal();

                //spawnList.unitRenderData[i].isNeedSpace = EditorGUILayout.Foldout(spawnList.unitRenderData[i].isNeedSpace, "");
                spawnList.unitRenderData[i].isNeedSpace = EditorGUILayout.Toggle(spawnList.unitRenderData[i].isNeedSpace, GUILayout.Width(toggleWidth));

                spawnList.DrawUnitField(i, unitFieldWidth);
                //EditorGUILayout.Separator();
                if (spawnList.unitsWeightMode == SpawnList.UnitsWeightMode.multiplyByCoefficient)
                {
                    spawnList.unitRenderData[i].unitWeightCoefficient = EditorGUILayout.FloatField(spawnList.unitRenderData[i].unitWeightCoefficient, GUILayout.Width(waveColumnWidth));
                }
                if(spawnList.isDisplayUnitDescription)
                {
                    if (spawnList.units[i] != null)
                    {
                        EditorGUILayout.LabelField(spawnList.units[i].GetDescription(), GUILayout.Width(defaultStringFieldWidth));
                    }
                    else
                    {
                        EditorGUILayout.LabelField("", GUILayout.Width(defaultStringFieldWidth));
                    }
                }
                EditorGUILayout.EndHorizontal();

                if(spawnList.unitRenderData[i].isNeedSpace)
                {
                    EditorGUILayout.Space(defaultSmallSpace);
                }
            }
            EditorGUILayout.Space(defaultSmallSpace);
            spawnList.DrawUnderUnitField(unitFieldWidth);


            EditorGUILayout.EndVertical();
        }
        void AddUnitFieldToSpawnList(SpawnList spawnList)
        {
            spawnList.AddUnitDataField();
        }
        void RemoveUnitFieldToSpawnList(SpawnList spawnList)
        {
            spawnList.RemoveUnitDataField();
        }

        void DrawWavesData(SpawnList spawnList)
        {
            EditorGUILayout.BeginHorizontal();
            // EditorGUILayout.LabelField("WAVES", GUILayout.Width(unitFieldWidth));

            for (int i = 0; i < spawnList.waves.Count; i++)
            {
                DrawWavesColumn(i);
                if(spawnList.waves[i].isDisplayRightSpace)
                {
                    EditorGUILayout.Space(defaultSmallSpace);
                }
            }

            EditorGUILayout.EndHorizontal();

        }
        void DrawWavesColumn(int waveInd)
        {
            EditorGUILayout.BeginVertical();


            DrawWavesMetrics(waveInd, spawnList, isWavesMetricsOpen);

            //EditorGUILayout.Space(defaultSmallSpace);

            //spawnList.waves[waveInd].isDisplayRightSpace = EditorGUILayout.Foldout(spawnList.waves[waveInd].isDisplayRightSpace,"");


            //centreText.fontSize = 12;
            EditorGUILayout.BeginHorizontal();
            spawnList.waves[waveInd].isDisplayRightSpace = EditorGUILayout.Toggle(spawnList.waves[waveInd].isDisplayRightSpace, GUILayout.Width(toggleWidth));

            string waveLabel;
            /*if(waveInd > 1 && spawnList.waves[waveInd-1].nextWaveCondition == SpawnList.NextWaveCondition.connectWaves)
            {
                waveLabel = "<" + (waveInd + 1).ToString();
            }
            else
            {*/
                waveLabel = (waveInd + 1).ToString();
           // }
            EditorGUILayout.LabelField(waveLabel, centreBoldText, GUILayout.Width(waveColumnWidth - toggleWidth));     
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.LabelField((waveInd + 1).ToString(), GUILayout.Width(waveColumnWidth));

            for (int j = 0; j < spawnList.units.Count; j++)
            {
                spawnList.waves[waveInd].unitsNum[j] = EditorGUILayout.IntField(spawnList.waves[waveInd].unitsNum[j], rightFieldText, GUILayout.Width(waveColumnWidth));
                if (spawnList.unitRenderData[j].isNeedSpace)
                {
                    EditorGUILayout.Space(defaultSmallSpace);
                }
            }

            EditorGUILayout.Space(defaultSmallSpace);
            spawnList.DrawUnderWaveColumn(waveInd, waveColumnWidth);

            

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
                Keyframe keyframe = new Keyframe(i + 1, curveFunction(i), 0,0,0.3f,0.3f);
                animationCurve.AddKey(keyframe);
            }

            EditorGUILayout.CurveField(animationCurve, GUILayout.Width(width), GUILayout.Height(width / 2));
        }
        void DrawWavesMetricsLabels(SpawnList spawnList)
        {
            isWavesMetricsOpen = EditorGUILayout.Foldout(isWavesMetricsOpen, waveMetricsFoldoutName);

            
            if (isWavesMetricsOpen)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(waveWeightMetricsName, GUILayout.Width(unitFieldWidth));
                EditorGUILayout.LabelField(waveTimeMetricsName, GUILayout.Width(unitFieldWidth));
                EditorGUILayout.LabelField(wavePowerMetricsName, GUILayout.Width(unitFieldWidth));
                EditorGUILayout.EndVertical();
            }
        }
        void DrawWavesMetrics(int waveInd, SpawnList spawnList, bool isDrawWavesMetrics)
        {
            EditorGUILayout.LabelField("", GUILayout.Width(waveColumnWidth));
            if (isDrawWavesMetrics)
            {
                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.LabelField(spawnList.GetWeightInWave(waveInd).ToString(), GUILayout.Width(waveColumnWidth));
                EditorGUILayout.LabelField(spawnList.GetTimeInWave(waveInd).ToString(), GUILayout.Width(waveColumnWidth));

                float wavePower = spawnList.GetPowerInWave(waveInd);
                string wavePowerString = (float.IsNaN(wavePower) || float.IsInfinity(wavePower)) ? "∞" : wavePower.ToString();
                EditorGUILayout.LabelField(wavePowerString.ToString(), GUILayout.Width(waveColumnWidth));

                EditorGUILayout.EndVertical();
            }
        }
    }
}