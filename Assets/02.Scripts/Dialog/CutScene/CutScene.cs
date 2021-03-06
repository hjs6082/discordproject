using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue
{
    public class CutScene : MonoBehaviour
    {
        private const float DEFAULT_DELAY = 1.0f;
        private const string CS_PATH = "CutSceneImages/";

        private StringBuilder sb = new StringBuilder();

        [SerializeField]
        private GameObject next_Text;
        [SerializeField]
        private GameObject dialogue_Object;
        private int CS_Order = 0;
        private Image CS_Image = null;
        public List<Sprite> CS_List = new List<Sprite>();
        private float delay_Time = 0.0f;

        private void Awake()
        {
            CS_Image = GetComponent<Image>();

            int CS_Amount = Resources.LoadAll<Sprite>(CS_PATH).Length;
            for (int i = 1; i <= CS_Amount; i++)
            {
                sb.Clear();
                sb.Append(CS_PATH).Append("CS_").Append(i.ToString());
                Sprite CS_Sprite = Resources.Load<Sprite>(sb.ToString());
                CS_List.Add(CS_Sprite);
            }

            GameManager.Instance?.FadePanel.SetActive(false);
        }

        private void Start()
        {
            InitValue();
            ChangeCS();
            FadeText();
        }

        private void Update()
        {
            if (CutDelay() <= 0.0f)
            {
                if (!next_Text.activeSelf)
                {
                    next_Text.SetActive(true);
                }

                InputCut();
            }
            else
            {
                if (next_Text.activeSelf)
                {
                    next_Text.SetActive(false);
                }
            }
        }

        private void InitValue()
        {
            CS_Order = 0;
            delay_Time = DEFAULT_DELAY;
            CS_Image.color = Color_Util.SetColor(1, 1, 1, 0);
            dialogue_Object.SetActive(false);
        }

        private float CutDelay()
        {
            delay_Time -= Time.deltaTime;

            return delay_Time;
        }

        private void InputCut()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextCut();
            }
        }

        private void NextCut()
        {
            ChangeCS();

            delay_Time = DEFAULT_DELAY;
            CS_Order++;
        }

        private void ChangeCS()
        {
            CS_Image.DOFade(0.0f, 0.75f).OnComplete(() =>
            {
                if (CS_Order < CS_List.Count)
                {
                    CS_Image.sprite = CS_List[CS_Order];
                    CS_Image.DOFade(1.0f, 0.75f);
                }
                else
                {
                    Skip();
                }
            });
        }

        public void Skip()
        {
            GameManager.Instance.Fade_OutIn(0.5f, () =>
            {
                dialogue_Object.SetActive(true);

                CS_Image.transform.parent.gameObject.SetActive(false);
            });
        }

        private void FadeText()
        {
            Text text = next_Text.GetComponent<Text>();

            text.DOFade(0.1f, 0.75f).OnComplete(() =>
            {
                text.DOFade(1.0f, 0.75f);
            }).SetLoops(-1, LoopType.Yoyo);
        }
    }
}