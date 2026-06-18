using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIV_PopUP_Tutorial : UIV_PopUP<TutorialPopupData>
{
    [SerializeField] private TMP_Text m_text;
    private Color m_textColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] private Image m_image;
    private Color m_imageColor = new Color(1f, 1f, 1f, 1f);

    private float m_duraion;
    private float m_elapsed;

    bool fadeIn = false;
    bool fadeOut = false;

    float m_fadeOutDuration;


    private void Update()
    {
        const float speed = 5f;
        if (fadeIn) 
        {
            m_image.color = Color.Lerp(m_image.color, m_imageColor, speed * Time.deltaTime);
            m_text.color = Color.Lerp(m_text.color, m_textColor, speed * Time.deltaTime);

            if (m_elapsed >= m_duraion) 
            {
                fadeIn = false;
                fadeOut = true;
            }
        }
        else if (fadeOut) 
        {
            if (m_elapsed >= m_duraion + m_fadeOutDuration) 
            {
                Close();                
                return;
            }
            m_image.color = Color.Lerp(m_image.color, new Color(0f, 0f, 0f, 0f), speed * Time.deltaTime);
            m_text.color = Color.Lerp(m_text.color, new Color(0f, 0f, 0f, 0f), speed * Time.deltaTime);
        }
        m_elapsed += Time.deltaTime;
    }

    protected override void BindTyped(TutorialPopupData data)
    {
        m_image.sprite = data.sprite;
        m_text.text = data.text;
        m_duraion = data.Duration;
        m_fadeOutDuration = data.Duration == 0 ? 0 : data.Duration / 4;
    }
    public override void Show()
    {
        m_elapsed = 0f;
        fadeIn = true;
        fadeOut = false;

        m_image.color = new Color(0f, 0f, 0f, 0f);
        m_text.color = new Color(0f, 0f, 0f, 0f);

        base.Show();
    }
    public override void Close()
    {
        fadeIn = false;
        fadeOut = false;
        m_elapsed = 0f;
        base.Close();

        m_image.color = m_imageColor;
        m_text.color = m_textColor;
    }
}