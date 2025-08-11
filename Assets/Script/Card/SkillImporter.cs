using UnityEngine;
using UnityEditor;
using System.IO;

public enum ResourceType
{
    None,
    Stamina,
    Focus,
    Hp,
    Momentum,
    Power
}

public enum TriggerCondition
{
    OnActivate,
    AfterActivate,
    OnAttackLand,
    OnAttackLandClean,
    OnOpponentJab,
    OnOpponentHook,
    OnOpponentUpper,
    AtLongRange,
    AtMidRange,
    AtCloseRange,
    AtHalfHp,
    AtHalfStamina,
    AtHalfFocus,
    HaveBleeding,
}

public enum Distance
{
    Away = 4,
    Long = 3,
    Mid = 2,
    Close = 1
}

public enum Movement
{
    None,
    Front,
    Left,
    Right,
    Back
}

[System.Serializable]
public class SkillData : ScriptableObject
{
    public int id;
    public string skillName;
    public Distance range;
    public ResourceType costType;
    public int cost;
    public int power;
    public int speed;
    public TriggerCondition triggerCondition;
    public ResourceType gainingType;
    public int gainingValue;
    [TextArea] public string description;
    public bool IsCounter;
    public bool IsFeint;
    public bool IsPenetrate;
    public bool IsBleed;
    public bool IsStun;
    public Movement movement;
    public Movement opponentPush;
}

public class SkillCSVImporter
{
    [MenuItem("Tools/Import Skills from CSV (ID Support)")]
    public static void ImportSkills()
    {
        string csvPath = Application.dataPath + "/skills.csv";
        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV file not found at: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        string folderPath = "Assets/Skills";

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Skills");
        }

        // Skip header
        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');

            if (data.Length < 8)
            {
                Debug.LogWarning($"Line {i + 1} skipped — not enough fields.");
                continue;
            }

            int id = int.Parse(data[0].Trim());
            string skillName = data[1].Trim();
            Distance range = (Distance)System.Enum.Parse(typeof(Distance), data[2].Trim(),true);
            ResourceType costType = (ResourceType)System.Enum.Parse(typeof(ResourceType), data[3].Trim());         
            int cost = int.Parse(data[4].Trim());
            int power = int.Parse(data[5].Trim());
            int speed = int.Parse(data[6].Trim());
            TriggerCondition triggerCondition = (TriggerCondition)System.Enum.Parse(typeof(TriggerCondition), data[7].Trim());
            ResourceType gainingType = (ResourceType)System.Enum.Parse(typeof(ResourceType), data[8].Trim());
            int gainingValue = int.Parse(data[9].Trim());
            string description = data[10].Trim();
            Debug.Log(data[10]);
            bool IsCounter = bool.Parse(data[11].Trim());
            bool IsFeint = bool.Parse(data[12].Trim());
            bool IsPenetrate = bool.Parse(data[13].Trim());
            bool IsBleed = bool.Parse(data[14].Trim());
            bool IsStun = bool.Parse(data[15].Trim());
            Movement movement = (Movement)System.Enum.Parse(typeof(Movement), data[16].Trim());
            Movement opponentPush = (Movement)System.Enum.Parse(typeof(Movement), data[17].Trim());

            string assetPath = $"{folderPath}/{id}_{skillName}.asset";
            SkillData skill = AssetDatabase.LoadAssetAtPath<SkillData>(assetPath);

            if (skill == null)
            {
                skill = ScriptableObject.CreateInstance<SkillData>();
                skill.id = id;
                skill.skillName = skillName;
                skill.range = range;
                skill.costType = costType;
                skill.cost = cost;
                skill.power = power;
                skill.speed = speed;
                skill.triggerCondition = triggerCondition;
                skill.description = description;
                skill.gainingType = gainingType;
                skill.gainingValue = gainingValue;
                skill.IsCounter = IsCounter;
                skill.IsFeint = IsFeint;
                skill.IsPenetrate = IsPenetrate;
                skill.IsBleed = IsBleed;
                skill.IsStun = IsStun;
                skill.movement = movement;
                skill.opponentPush = opponentPush;

                AssetDatabase.CreateAsset(skill, assetPath);
                Debug.Log($"Created new skill: {skillName} (ID: {id})");
            }
            else
            {
                skill.id = id;
                skill.skillName = skillName;
                skill.range = range;
                skill.costType = costType;
                skill.cost = cost;
                skill.power = power;
                skill.speed = speed;
                skill.triggerCondition = triggerCondition;
                skill.description = description;
                skill.gainingType = gainingType;
                skill.gainingValue = gainingValue;
                skill.IsCounter = IsCounter;
                skill.IsFeint = IsFeint;
                skill.IsPenetrate = IsPenetrate;
                skill.IsBleed = IsBleed;
                skill.IsStun = IsStun;
                skill.movement = movement;
                skill.opponentPush = opponentPush;

                EditorUtility.SetDirty(skill);
                Debug.Log($"Updated skill: {skillName} (ID: {id})");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Skill import complete.");
    }
}
