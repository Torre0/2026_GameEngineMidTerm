using System.Linq;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RankPage : MonoBehaviour
{
    public int level = 1;

    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefab;

    StageResultList allData;

    void Awake()
        {
            allData = StageResultSaver.LoadRank( );
            RefreshRankList();
        }
    void RefreshRankList()
    {
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

    var sortedData = allData.results.Where(r => r.stage == level).OrderByDescending(x => x.score).ToList();

    for (int i = 0; i<sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
