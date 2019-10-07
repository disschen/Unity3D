
public class Referee: MonoBehaviour{
    
	public GenGameObject object;
	public int state = 0;
	
    public void check()
    {
		
        int priests_s = 0, devils_s = 0, priests_e = 0, devils_e = 0;
        for(int i = 0; i < 3; i++)
        {
            if(object.priests_start[i] != null)
            {
                priests_s++;
            }
            if(object.devils_start[i] != null)
            {
                devils_s++;
            }
            if(object.priests_end[i] != null)
            {
                priests_e++;
            }
            if(object.devils_end[i] != null)
            {
                devils_e++;
            }
        }
        if(((priests_s < devils_s) && (priests_s != 0))||((priests_e < devils_e) && (priests_e != 0)))
        {
            print("you lose");
            state = 1;
        }
        else if (priests_s == 0 && devils_s == 0)
        {
            print("you win!!!");
            state = 2;
        }
		if(state != 0) object.game = state;
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
