using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace TMPro.Examples
{

    public class RollingTextFade : MonoBehaviour
    {

        private TMP_Text m_TextComponent;
        private TextMeshProUGUI TextPro;

        public float FadeSpeed = 1.0F;
        public int RolloverCharacterSpread = 10;
        public Color ColorTint;


        void Awake()
        {
            m_TextComponent = GetComponent<TMP_Text>();
        }


        void Start()
        {
            
        }

        public void Begin()
        {
            TextPro = GetComponent<TextMeshProUGUI>();
            TextPro.faceColor = new Color(TextPro.faceColor.r, TextPro.faceColor.g, TextPro.faceColor.b, 0);
            TextPro.outlineColor = new Color(TextPro.faceColor.r, TextPro.faceColor.g, TextPro.faceColor.b, 0);
            StartCoroutine(FadeTextToFullAlpha(2.0f));
        }

        public IEnumerator FadeTextToFullAlpha(float t)
        {
            while (TextPro.faceColor.a < 255.0f)
            {
                Color AuxColor = TextPro.faceColor;
                TextPro.faceColor = new Color(AuxColor.r, AuxColor.g, AuxColor.b, AuxColor.a + (Time.deltaTime / t));
                TextPro.outlineColor = new Color(AuxColor.r, AuxColor.g, AuxColor.b, AuxColor.a + (Time.deltaTime / t));
                yield return null;
            }
            StartCoroutine(AnimateVertexColors());
        }

        public IEnumerator FadeTextToZeroAlpha(float t, Text i)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }


        /// <summary>
        /// Method to animate vertex colors of a TMP Text object.
        /// </summary>
        /// <returns></returns>
        IEnumerator AnimateVertexColors()
        {
            // Need to force the text object to be generated so we have valid data to work with right from the start.
            m_TextComponent.ForceMeshUpdate();

            yield return new WaitForSeconds(0.1f);

            TMP_TextInfo textInfo = m_TextComponent.textInfo;
            Color32[] newVertexColors;

            int currentCharacter = 0;
            int startingCharacterRange = currentCharacter;
            bool isRangeMax = false;

            while (!isRangeMax)
            {
                int characterCount = textInfo.characterCount;

                // Spread should not exceed the number of characters.
                byte fadeSteps = (byte)Mathf.Max(1, 255 / RolloverCharacterSpread);


                for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
                {
                    // Skip characters that are not visible
                    if (!textInfo.characterInfo[i].isVisible) continue;

                    // Get the index of the material used by the current character.
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                    // Get the vertex colors of the mesh used by this text element (character or sprite).
                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                    // Get the index of the first vertex used by this text element.
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    // Get the current character's alpha value.
                    byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a - fadeSteps, 0, 255);

                    // Set new alpha values.
                    newVertexColors[vertexIndex + 0].a = alpha;
                    newVertexColors[vertexIndex + 1].a = alpha;
                    newVertexColors[vertexIndex + 2].a = alpha;
                    newVertexColors[vertexIndex + 3].a = alpha;

                    // Tint vertex colors
                    // Note: Vertex colors are Color32 so we need to cast to Color to multiply with tint which is Color.
                    newVertexColors[vertexIndex + 0] = (Color)newVertexColors[vertexIndex + 0] * ColorTint;
                    newVertexColors[vertexIndex + 1] = (Color)newVertexColors[vertexIndex + 1] * ColorTint;
                    newVertexColors[vertexIndex + 2] = (Color)newVertexColors[vertexIndex + 2] * ColorTint;
                    newVertexColors[vertexIndex + 3] = (Color)newVertexColors[vertexIndex + 3] * ColorTint;

                    if (alpha == 0)
                    {
                        startingCharacterRange += 1;

                        if (startingCharacterRange == characterCount)
                        {
                            
                            // Update mesh vertex data one last time.
                            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                            yield return new WaitForSeconds(1.0f);
                            Destroy(this.gameObject);

                            // Reset the text object back to original state.
                            m_TextComponent.ForceMeshUpdate();

                            yield return new WaitForSeconds(1.0f);

                            // Reset our counters.
                            currentCharacter = 0;
                            startingCharacterRange = 0;
                            isRangeMax = true; // Would end the coroutine.
                            
                        }
                    }
                }

                // Upload the changed vertex colors to the Mesh.
                m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                if (currentCharacter + 1 < characterCount) currentCharacter += 1;

                yield return new WaitForSeconds(0.25f - FadeSpeed * 0.01f);

            }
            Destroy(this.gameObject);
        }
    }
}
