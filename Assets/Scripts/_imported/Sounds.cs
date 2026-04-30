using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pacman
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "Pacman/Sounds")]
    public class Sounds : ScriptableObject
    {
        public  AudioClip[] m_Sounds;
        public AudioClip this[Sound s] => m_Sounds[(int) s];
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Sounds))]
    public class SoundInspector : Editor
    {
        private static readonly int soundCount =
            Enum.GetValues(typeof(Sound)).Length;
        private new Sounds target => base.target as Sounds;

        public override void OnInspectorGUI()
        {
            
            if (target == null) return;

            
            if (target.m_Sounds == null)
                target.m_Sounds = new AudioClip[soundCount];

            if (target.m_Sounds.Length < soundCount)
                Array.Resize(ref target.m_Sounds, soundCount);

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < soundCount; i++)
            {
                target.m_Sounds[i] = EditorGUILayout.ObjectField(
                    $"{(Sound)i}",
                    target.m_Sounds[i],
                    typeof(AudioClip),
                    false) as AudioClip;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif

}
