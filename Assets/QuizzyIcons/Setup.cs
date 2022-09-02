using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Setup
{
    public static void InitializeDragDrop(VisualElement root, Controller controller)
    {
        root.Query<VisualElement>("IconBoard")
            .Children<VisualElement>()
            .ForEach((elem) =>
            {
                elem.AddManipulator(new IconDragger(root, controller));
            });
    }

    public static void InitializeIcons(VisualElement root, List<Question> questions)
    {
        int currentIconIndex = 0;

        foreach (Question question in questions)
        {
            VisualElement questionIcon = root.Query<VisualElement>("IconBoard").Children<VisualElement>().AtIndex(currentIconIndex);
            questionIcon.style.backgroundImage = Resources.Load<Texture2D>("img/" + question.answer);
            questionIcon.userData = question;

            currentIconIndex++;
        }
    }


}