using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ArabicSupport;

public class EnemyInstantiation : MonoBehaviour
{
    public GameObject crabPrefab;
    public GameObject scabiesPrefab;
    public GameObject bombPrefab;

    public GameObject parentObject;

    public TextMeshProUGUI fact;
    public GameObject panel;
    public GameObject panelGameOver;

    private List<string> crabFacts;
    private List<string> scabiesFacts;

    public List<string> crabFactsArabic;
    public List<string> scabiesFactsArabic;

    public List<string> crabFactsGreek;
    public List<string> scabiesFactsGreek;

    private int crabsCollected;
    private int scabiesCollected;

    public TextMeshProUGUI scoreCrabs;
    public TextMeshProUGUI scoreScabies;

    public GameObject scorePanel;

    public TextMeshProUGUI scoreCrabsOver;
    public TextMeshProUGUI scoreScabiesOver;

    public Joystick joystick;

    public GameObject player;

    string language;

    int check;

    // Start is called before the first frame update
    void Start()
    {
        check = 0;
        language = PlayerPrefs.GetString("language");
        crabsCollected = 0;
        scabiesCollected = 0;

        crabFacts = new List<string>();
        scabiesFacts = new List<string>();

        switch (language)
        {
            case "ar":
                crabFacts = new List<string>(crabFactsArabic);
                scabiesFacts = new List<string>(scabiesFactsArabic);
                break;
            case "fr":
                crabFacts.Add("Les poux du pubis sont également connus sous le nom de \"crabes \".");
                crabFacts.Add("Les poux du pubis sont des parasites ressemblant à des crabes qui vivent sur les poils pubiens.");
                crabFacts.Add("Les préservatifs ne vous protègent pas des poux du pubis.");
                crabFacts.Add("Les poux du pubis mesurent environ 2 mm. Ils peuvent être très difficiles à voir.");
                crabFacts.Add("Les poux du pubis peuvent se trouver dans d'autres zones poilues du corps, comme les jambes, le dos ou les aisselles.");
                crabFacts.Add("Les poux du pubis ne nécessitent pas de contact sexuel pour être transmis. Un contact corporel étroit peut suffire");
                crabFacts.Add("Le partage des serviettes et du linge de lit peut provoquer une infestation de poux du pubis.");
                crabFacts.Add("Les poux du pubis ne peuvent pas transmettre d'autres IST.");
                crabFacts.Add("Les poux du pubis ne peuvent pas sauter (contrairement aux puces).");
                crabFacts.Add("Les poux du pubis ne peuvent pas vivre dans les cheveux.");
                crabFacts.Add("Le dépistage des poux du pubis se fait par l'examen physique de la zone pubienne par un professionnel.");
                crabFacts.Add("Les poux du pubis ne disparaissent pas d'eux-mêmes. Ils doivent être traités.");
                crabFacts.Add("Le traitement des poux du pubis consiste en des shampooings, lotions et crèmes spécialisés.");
                crabFacts.Add("Le rasage de la zone pelvienne peut aider, mais ne vous protège pas totalement des infestations.");

                scabiesFacts.Add("La gale est causée par un acarien parasite, Sarcoptes scabiei.");
                scabiesFacts.Add("La gale se transmet par contact de peau à peau. Il n'est pas nécessaire qu'il s'agisse d'un contact sexuel.");
                scabiesFacts.Add("Le partage des serviettes et du linge de lit peut provoquer une infestation par la gale.");
                scabiesFacts.Add("Les acariens de la gale sont minuscules. Presque impossibles à voir à l'œil nu");
                scabiesFacts.Add("La gale infeste les plis de la peau, comme les organes génitaux, les fesses, les genoux, les mains, les poignets et les doigts.");
                scabiesFacts.Add("Un préservatif ne vous protège pas d'une infestation par la gale.");
                scabiesFacts.Add("Le traitement de la gale consiste en des shampooings, lotions et crèmes spécialisés.");
                scabiesFacts.Add("La gale et les poux du pubis sont souvent confondus.");
                scabiesFacts.Add("Lorsqu'un membre de la famille est atteint de la gale, toute la famille doit suivre le traitement.");
                scabiesFacts.Add("Un test de dépistage de la gale nécessite un examen physique par un professionnel de la santé.");
                break;
            case "el":
                crabFacts = new List<string>(crabFactsGreek);
                scabiesFacts = new List<string>(scabiesFactsGreek);
                break;
            default:
                crabFacts.Add("Pubic lice are also known as Crabs.");
                crabFacts.Add("Pubic lice are crab-like parasites who live on pubic hair.");
                crabFacts.Add("Condoms will not protect you from pubic lice.");
                crabFacts.Add("Pubic lice measure about 2mm. They can be very hard to see.");
                crabFacts.Add("Pubic lice can be found in other hairy areas of the body, such as legs, back or armpits.");
                crabFacts.Add("Pubic lice do not require sexual contact to be passed on. Close body contact might be enough.");
                crabFacts.Add("Sharing towels and bed linen can cause a pubic lice infestation.");
                crabFacts.Add("Pubic lice cannot transmit other STIs.");
                crabFacts.Add("Pubic lice cannot jump (unlike fleas).");
                crabFacts.Add("Pubic lice cannot live in hair.");
                crabFacts.Add("Testing for pubic lice is done through the physical examination of the pubic area by a professional.");
                crabFacts.Add("Pubic lice will not go away on their own. They require treatment.");
                crabFacts.Add("Pubic lice treatment consists of specialised shampoos, lotions and creams.");
                crabFacts.Add("Shaving the pelvic area might help, but it won't fully protect you from infestations.");
                crabFacts.Add("Pubic lice can cause intense itching, but are treatable and don’t cause serious health concerns.");
                crabFacts.Add("You most likely cannot get crabs by sharing a toilet seat with someone who has them.");
                crabFacts.Add("Pubic lice symptoms often show up about five days after you get infested.");
                crabFacts.Add("The only guaranteed way to prevent pubic lice is to avoid any close physical contact with people.");
                crabFacts.Add("Make sure your partner(s) get treated if you had pubic lice.");
                crabFacts.Add("Usually, the main problems that the lice cause are itching and discomfort. You may get a bacterial infection if you end up scratching your skin a lot.");

                scabiesFacts.Add("Scabies is caused by the parasite mite Sarcoptes scabiei.");
                scabiesFacts.Add("Scabies is passed by skin-to-skin contact. It does not need to be sexual contact.");
                scabiesFacts.Add("Sharing towels and bed linen can cause a scabies infestation.");
                scabiesFacts.Add("Scabies mites are tiny. Almost impossible to see with the naked eye.");
                scabiesFacts.Add("Scabies infests skin folds, such as genitals, buttocks, knees, hands, wrists, fingers.");
                scabiesFacts.Add("A condom will not protect you from a scabies infestation.");
                scabiesFacts.Add("Scabies treatment consists of specialised shampoos, lotions and creams.");
                scabiesFacts.Add("Scabies and pubic lice are commonly confused with one another.");
                scabiesFacts.Add("When someone in a household gets scabies, the entire household should go through the treatment.");
                scabiesFacts.Add("A scabies test requires a physical examination by a medical professional.");
                scabiesFacts.Add("The symptoms of scabies are caused by the female mites, which tunnel into the skin after being fertilized.");
                scabiesFacts.Add("A person who is infected with scabies typically has around 12 mites at any given time.");
                scabiesFacts.Add("It takes about three to four weeks for signs or symptoms of a first scabies infection to develop after infection.");
                break;
        }        

        CreateCrab(30, -2);
        CreateCrab(60, -1);
        CreateScabies(20, 0);
        CreateScabies(40, -4);
        CreateBomb(25, 3);
        CreateBomb(50, -3);
        CreateBomb(75, 4);
        CreateBomb(100, -1);
    }

    private void Update()
    {
        GameObject[] crabs = GameObject.FindGameObjectsWithTag("Crab");
        GameObject[] scabies = GameObject.FindGameObjectsWithTag("Scabies");
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");

        foreach(GameObject c in crabs)
        {
            if (c.transform.position.x < player.transform.position.x - 20)
            {
                c.transform.position = new Vector2(c.transform.position.x + 30, c.transform.position.y);
            }
        }

        foreach (GameObject s in scabies)
        {
            if (s.transform.position.x < player.transform.position.x - 20)
            {
                s.transform.position = new Vector2(s.transform.position.x + 50, s.transform.position.y);
            }
        }

        foreach (GameObject b in bombs)
        {
            if (b.transform.position.x < player.transform.position.x - 20)
            {
                b.transform.position = new Vector2(b.transform.position.x + 40, b.transform.position.y);
            }
        }
    }

    void CreateCrab(int offsetX, int offsetY)
    {
        Vector2 position = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        (Instantiate(crabPrefab, position, Quaternion.identity) as GameObject).transform.parent = parentObject.transform;
    }

    void CreateScabies(int offsetX, int offsetY)
    {
        Vector2 position = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        (Instantiate(scabiesPrefab, position, Quaternion.identity) as GameObject).transform.parent = parentObject.transform;
    }

    void CreateBomb(int offsetX, int offsetY)
    {
        Vector2 position = new Vector2(transform.position.x + offsetX, transform.position.y + offsetY);
        (Instantiate(bombPrefab, position, Quaternion.identity) as GameObject).transform.parent = parentObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crab"))
        {
            collision.gameObject.SetActive(false);
            if (crabsCollected > crabFacts.Count - 1)
            {
                if (language == "ar")
                {
                   fact.SetText(ArabicFixer.Fix(crabFacts[crabsCollected - crabFacts.Count]));
                } else
                {
                   fact.SetText(crabFacts[crabsCollected - crabFacts.Count]);
                }
               
            }
            else
            {
                if (language == "ar")
                {
                    fact.SetText(ArabicFixer.Fix(crabFacts[crabsCollected]));
                }
                else
                {
                    fact.SetText(crabFacts[crabsCollected]);
                }
            }

            panel.SetActive(true);
            crabsCollected++;
            scoreCrabs.SetText(crabsCollected.ToString());
            if (check == 0)
            {
                CreateCrab(30, -2);
                check = 1;
            } else
            {
                CreateCrab(60, 0);
                check = 0;
            }
            
        }

        if (collision.gameObject.CompareTag("Scabies"))
        {
            collision.gameObject.SetActive(false);
            if (scabiesCollected > scabiesFacts.Count - 1)
            {
                if (language == "ar")
                {
                    fact.SetText(ArabicFixer.Fix(scabiesFacts[scabiesCollected - scabiesFacts.Count]));
                }
                else
                {
                    fact.SetText(scabiesFacts[scabiesCollected - scabiesFacts.Count]);
                }
                
            }
            else
            {
                if (language == "ar")
                {
                    fact.SetText(ArabicFixer.Fix(scabiesFacts[scabiesCollected]));
                }
                else
                {
                    fact.SetText(scabiesFacts[scabiesCollected]);
                }
            }

            panel.SetActive(true);
            scabiesCollected++;
            scoreScabies.SetText(scabiesCollected.ToString());
            if (check == 0)
            {
                CreateScabies(30, -2);
                check = 1;
            }
            else
            {
                CreateScabies(60, -1);
                check = 0;
            }
            
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            collision.gameObject.SetActive(false);
            scorePanel.SetActive(false);
            scoreCrabsOver.SetText(crabsCollected.ToString());
            scoreScabiesOver.SetText(scabiesCollected.ToString()); 
            panelGameOver.SetActive(true);
            joystick.gameObject.SetActive(false);
            gameObject.SetActive(false);
            panel.SetActive(false);
        }
        
    }

}
