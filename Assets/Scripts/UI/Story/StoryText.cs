using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryText : MonoBehaviour
{


    [SerializeField] private TMP_Text storytext1;

    [SerializeField] private TMP_Text storytext2;
    [SerializeField] private TMP_Text quote;

    [SerializeField] private Image image;
    [SerializeField] private Color textColor = new Color(1, 1, 1, 1);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        storytext1.color = new Color(1, 1, 1, 0);
        storytext2.color = new Color(1, 1, 1, 0);
        quote.color = new Color(1, 1, 1, 0);
        image.color = new Color(1, 1, 1, 0);
        StartCoroutine(waiter());
    }


    IEnumerator waiter()
    {
        float duration = 1f;
        float elapsed = 0f;
        Color startColor = quote.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            quote.color = Color.Lerp(startColor, textColor, elapsed / duration);
            yield return null;
        }
        quote.color = textColor;

        // Wait for 2 seconds before starting to fade out
        yield return new WaitForSeconds(2);

        // Lerp text back to Color(1,1,1,0)
        duration = 1f;
        elapsed = 0f;
        Color endColor = new Color(1, 1, 1, 0);
        Color currentColor = quote.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            quote.color = Color.Lerp(currentColor, endColor, elapsed / duration);
            yield return null;
        }
        quote.color = endColor;


        yield return new WaitForSeconds(1f);
        // Fade in storytext1

        duration = 1f;
        elapsed = 0f;
        startColor = storytext1.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            storytext1.color = Color.Lerp(startColor, textColor, elapsed / duration);


            yield return null;
        }
        storytext1.color = textColor;

        // Wait for 2 seconds before starting to fade out
        yield return new WaitForSeconds(8);

        // Lerp text back to Color(1,1,1,0)
        duration = 1f;
        elapsed = 0f;
        endColor = new Color(1, 1, 1, 0);
        currentColor = storytext1.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            storytext1.color = Color.Lerp(currentColor, endColor, elapsed / duration);
            yield return null;
        }
        storytext1.color = endColor;

        yield return new WaitForSeconds(1f);
        // Fade in storytext2

        duration = 1f;
        elapsed = 0f;
        startColor = storytext2.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            storytext2.color = Color.Lerp(startColor, textColor, elapsed / duration);
            yield return null;
        }
        storytext2.color = textColor;

        // Wait for 8 seconds before starting to fade out
        yield return new WaitForSeconds(6);

        // Lerp text back to Color(1,1,1,0)
        duration = 1f;
        elapsed = 0f;
        endColor = new Color(1, 1, 1, 0);
        currentColor = storytext2.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            storytext2.color = Color.Lerp(currentColor, endColor, elapsed / duration);
            yield return null;
        }
        storytext2.color = endColor;

        // Wait for 1 second before starting to fade in the image
        yield return new WaitForSeconds(1f);

        // Fade in the image
        duration = 1f;
        elapsed = 0f;
        startColor = image.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(startColor, textColor, elapsed / duration);
            yield return null;
        }
        image.color = textColor;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
