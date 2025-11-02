using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<ObjectStory> interactableObjectsData = new List<ObjectStory>()
    {
        new ObjectStory
        {
            obj = null,
            name = "Réveil",
            story = "« On dirait le réveil de ma chambre d’enfant... mais pourquoi semble-t-il si vieux, maintenant ? »"
        },
        new ObjectStory
        {
            obj = null,
            name = "Chaise",
            story = "« La chaise de maman. Elle adorait le rose, alors je l’avais peinte moi-même.\n    Mais ces marques vertes... d’où viennent-elles ? »"
        },
    };
    
    //load in editor mode all the stories in the list above
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Réveil
    // « On dirait le réveil de ma chambre d’enfant... mais pourquoi semble-t-il si vieux, maintenant ? »
    // Chaise
    // « La chaise de maman. Elle adorait le rose, alors je l’avais peinte moi-même.
    //     Mais ces marques vertes... d’où viennent-elles ? »
    // Table basse
    // « Notre petite table à café.
    //     On aimait s’y retrouver tous ensemble pour bavarder autour d’un café. »
    // Buffet de cuisine
    // « Le meuble de la cuisine...
    // Depuis combien de temps n’ai-je pas goûté aux plats de mes parents ? »
    // Table de chevet
    // « Ah, ma table de chevet.
    //     Mais qu’est-ce que j’avais mis dans le tiroir, déjà ? »
    // Bocal bleu
    // « Le bocal à épices de maman.
    //     Je le trouvais si joli... Je me souviens avoir voulu y mettre des fleurs à la place. »
    // Toupie
    // « Ma toupie préférée, quand j’étais petit·e... Quelle nostalgie. »
    // Horloge
    // « L’horloge du salon.
    //     Je me rappelle qu’elle se tenait juste à côté de la télévision. »
    // Table en bois
    // « La table du jardin... Papa l’aimait beaucoup.
    //     Quand il pleuvait, il la rentrait toujours à l’intérieur. »
    // Pellicule
    // « La pellicule de papa.
    //     Il emportait toujours sa caméra, où qu’il aille. »
    // Boîte à musique
    // « Ma boîte à musique préférée.
    //     Je croyais qu’en l’écoutant avant de dormir, je ferais de beaux rêves. »
    // Tasse
    // « Oh, la tasse que j’avais achetée avec maman.
    //     Le petit cœur sur le côté, c’est moi qui l’avais dessiné. »
    // Bouilloire
    // « La bouilloire de maman.
    //     Chaque matin, elle faisait chauffer l’eau pour son thé.
    //     J’aimais rester là, à écouter le bruit de l’eau qui bout. »
    // Petite table
    // « Une petite table pour les fleurs...
    // Maman aimait y poser un vase.
    //     Cela fait si longtemps que je n’ai pas senti le parfum des fleurs fraîches. »
}
