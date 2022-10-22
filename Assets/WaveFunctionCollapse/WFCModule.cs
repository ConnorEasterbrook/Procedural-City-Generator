using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace WFCGenerator
{
    [CreateAssetMenu(menuName = "WFC/Module")]
    [System.Serializable]
    public class WFCModule : ScriptableObject
    {
        public GameObject[] prefab;
        [HideInInspector] public WFCConnection forward;
        [HideInInspector] public WFCConnection right;
        [HideInInspector] public WFCConnection back;
        [HideInInspector] public WFCConnection left;
        [HideInInspector] public WFCConnection above;
        [HideInInspector] public WFCConnection below;
        public bool rotate180;
        public bool randomRotation;
        public bool banForward = false;
        public bool banLeft = false;
        public bool banModules = false;
        public WFCModule[] bannedModules;
        [Range(0, 1)] public float probability = 1;

        public bool ConnectsTo(WFCModule other, int direction)
        {
            if (direction == 0)
            {
                return back.ConnectsTo(other.forward);
            }
            else if (direction == 1)
            {
                return left.ConnectsTo(other.right);
            }
            else if (direction == 2)
            {
                return forward.ConnectsTo(other.back);
            }
            else if (direction == 3)
            {
                return right.ConnectsTo(other.left);
            }
            else if (direction == 4)
            {
                return above.ConnectsTo(other.below);
            }
            else if (direction == 5)
            {
                return below.ConnectsTo(other.above);
            }
            else
            {
                throw new System.ArgumentException("Invalid direction");
            }
        }

        public bool CheckBannedNeighbour(WFCModule other, int direction)
        {
            if (direction == 0)
            {
                if (banModules)
                {
                    bool banned = false;
                    for (int bannedNeighbourModule = 0; bannedNeighbourModule < bannedModules.Length; bannedNeighbourModule++)
                    {
                        if (back.ConnectsTo(other.forward) && bannedModules[bannedNeighbourModule] == other)
                        {
                            banned = true;
                        }
                    }

                    if (back.ConnectsTo(other.forward) && !banned)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else if (direction == 1)
            {
                if (banModules)
                {
                    bool banned = false;
                    for (int bannedNeighbourModule = 0; bannedNeighbourModule < bannedModules.Length; bannedNeighbourModule++)
                    {
                        if (left.ConnectsTo(other.right) && bannedModules[bannedNeighbourModule] == other)
                        {
                            banned = true;
                        }
                    }

                    if (left.ConnectsTo(other.right) && !banned)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else if (direction == 2)
            {
                if (banModules)
                {
                    bool banned = false;
                    for (int bannedNeighbourModule = 0; bannedNeighbourModule < bannedModules.Length; bannedNeighbourModule++)
                    {
                        if (forward.ConnectsTo(other.back) && bannedModules[bannedNeighbourModule] == other)
                        {
                            banned = true;
                        }
                    }

                    if (forward.ConnectsTo(other.back) && !banned)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else if (direction == 3)
            {
                if (banModules)
                {
                    bool banned = false;
                    for (int bannedNeighbourModule = 0; bannedNeighbourModule < bannedModules.Length; bannedNeighbourModule++)
                    {
                        if (right.ConnectsTo(other.left) && bannedModules[bannedNeighbourModule] == other)
                        {
                            banned = true;
                        }
                    }

                    if (right.ConnectsTo(other.left) && !banned)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new System.ArgumentException("Invalid direction");
            }
        }

        public bool CheckDuplicateRestriction(WFCModule other, int direction)
        {
            if (direction == 0)
            {
                if (back.ConnectsTo(other.forward) && !banForward)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (direction == 1)
            {
                if (left.ConnectsTo(other.right) && !banLeft)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (direction == 2)
            {
                if (forward.ConnectsTo(other.back) && !banForward)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (direction == 3)
            {
                if (right.ConnectsTo(other.left) && !banLeft)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new System.ArgumentException("Invalid direction");
            }
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(WFCModule))]
    public class WFCModuleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            WFCModule module = (WFCModule)target;

            serializedObject.FindProperty("forward.name").stringValue = EditorGUILayout.TextField("Forward", module.forward.name);
            serializedObject.FindProperty("right.name").stringValue = EditorGUILayout.TextField("Right", module.right.name);
            serializedObject.FindProperty("back.name").stringValue = EditorGUILayout.TextField("Back", module.back.name);
            serializedObject.FindProperty("left.name").stringValue = EditorGUILayout.TextField("Left", module.left.name);
            serializedObject.FindProperty("above.name").stringValue = EditorGUILayout.TextField("Above", module.above.name);
            serializedObject.FindProperty("below.name").stringValue = EditorGUILayout.TextField("Below", module.below.name);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
