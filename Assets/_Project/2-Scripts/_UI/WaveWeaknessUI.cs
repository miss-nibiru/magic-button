using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows what monsters are spawning in the current wave
/// and what spells they are weak to.
/// Banners stay visible until the next wave replaces them.
/// </summary>
public class WaveWeaknessUI : MonoBehaviour
{
    [SerializeField] private MainGrid mainGrid;
    [SerializeField] private GameObject weaknessBanner;

    [SerializeField] private int startingColumn;
    [SerializeField] private float verticalOffset;
    [SerializeField] private int columnSpacing;

    private List<GameObject> _activeBanners = new List<GameObject>();

    public void ShowWaveWeaknessBanners(GameObject[] monsterPrefabs)
    {
        ClearBanners();

        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            GameObject monsterPrefab = monsterPrefabs[i];
            if (!monsterPrefab) continue;

            MonsterController monsterController = monsterPrefab.GetComponent<MonsterController>();
            if (!monsterController) continue;

            SpellData weaknessSpell = monsterController.MonsterData.MonsterWeakness;
            if (!weaknessSpell) continue;

            int column = startingColumn - (i * columnSpacing);

            Vector3 bannerPosition = mainGrid.GetColumnLocation(column);
            bannerPosition += new Vector3(0f, verticalOffset, 0f);

            GameObject banner = Instantiate(weaknessBanner, bannerPosition, Quaternion.identity);

            SpriteRenderer spriteRenderer = banner.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                spriteRenderer.sprite = weaknessSpell.SpellWaveBanner;
            }

            _activeBanners.Add(banner);
        }
    }

    public void ShowWaveWeaknessBanners(GameObject monsterPrefab)
    {
        ShowWaveWeaknessBanners(new GameObject[] { monsterPrefab });
    }

    public void ClearBanners()
    {
        for (int i = 0; i < _activeBanners.Count; i++)
        {
            if (_activeBanners[i])
            {
                Destroy(_activeBanners[i]);
            }
        }

        _activeBanners.Clear();
    }
}