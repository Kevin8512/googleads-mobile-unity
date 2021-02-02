// Copyright (C) 2019 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using UnityEditor;
using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

namespace GoogleMobileAds.Editor
{
    [CustomEditor(typeof(BannerAdGameObject))]
    [CanEditMultipleObjects]
    public class BannerAdGameObjectEditor : AdGameObjectEditor
    {

        private bool showAppearance = true;

        private SerializedProperty propAdSize;

        private SerializedProperty propAdPosition;

        private SerializedProperty propAdPostionOffset;

        private SerializedProperty propAdType;

        public override void OnEnable()
        {
            propAdType = serializedObject.FindProperty("adType");
            propAdType.enumValueIndex = (int)AdPlacement.AdType.Banner;

            base.OnEnable();

            propAdSize = serializedObject.FindProperty("adSize");
            propAdPosition = serializedObject.FindProperty("adPosition");
            propAdPostionOffset = serializedObject.FindProperty("adPositionOffset");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();
            showAppearance = EditorGUILayout.Foldout(showAppearance, "Banner configuration");
            if (showAppearance)
            {
                EditorGUILayout.PropertyField(propAdSize);

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(propAdPosition);

                AdPosition currPosition = (AdPosition)Enum.ToObject(typeof(AdPosition), propAdPosition.intValue);
                if (EditorGUI.EndChangeCheck())
                {
                }

                if (currPosition == AdPosition.Custom)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(propAdPostionOffset, new GUIContent("Offset (dp)"));
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.Separator();
            showCallbacks = EditorGUILayout.Foldout(showCallbacks, "Callbacks");
            if (showCallbacks)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onAdLoaded"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onAdFailedToLoad"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onAdOpening"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onAdClosed"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onAdLeavingApplication"));
            }

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
