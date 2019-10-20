﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class FirstController : MonoBehaviour, SceneController, UserAction
{
    public UFOFactor disk_factory;
    public UserGUI user_gui;
	
    private int round = 1;                                                   //回合
    private float speed = 2f;                                                //发射一个飞碟的时间间隔
    private int curScore = 0;
	private int curLife = 6;
	private bool playing_game = false;                                       //游戏中
    private bool game_over = false;                                          //游戏结束
    private bool game_start = false;                                         //游戏开始
	private int score_round2 = 10;                                           //去到第二回合所需分数
    private int score_round3 = 25;                                           //去到第三回合所需分数
	private List <UFO> this_trival_ufos = new List<UFO>();
	private List <UFO> disk_notshot = new List<UFO>();
	
    void Start ()
    {
		Debug.Log("right here FirstController 1");
        Director director = Director.getInstance();     
        director.currentSceneController = this;             
        disk_factory = Singleton<UFOFactor>.Instance;
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }
	
	void Update ()
    {
        if(game_start)
        {
            //游戏结束，取消定时发送飞碟
            if (game_over)
            {
                CancelInvoke("LoadResources");
            }
            //设定一个定时器，发送飞碟，游戏开始
            if (!playing_game)
            {
				Debug.Log("right here FirstController 2");
                InvokeRepeating("LoadResources", 1f, speed);
                playing_game = true;
            }
            //发送飞碟
            FlyUFO();
            //回合升级
            if (curScore >= score_round2 && round == 1)
            {
				Debug.Log("level up");
                round = 2;
                //缩小飞碟发送间隔
                speed = speed - 0.6f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
            else if (curScore >= score_round3 && round == 2)
            {
                round = 3;
                speed = speed - 0.3f;
                CancelInvoke("LoadResources");
                playing_game = false;
            }
        }
    }

	public void FlyUFO() {
		if (game_over) return;
		// Debug.Log("right here FirstController 2");
		// Debug.Log(this_trival_ufos.Count);
		for (int i = 0;i < this_trival_ufos.Count;i ++){
			
			this_trival_ufos[i].ufo.SetActive(true);
			disk_notshot.Add(this_trival_ufos[i]);
			
			this_trival_ufos[i].Fly();
			
			this_trival_ufos.Remove(this_trival_ufos[i]);
			i --;
		}
		Debug.Log("count");
		Debug.Log(disk_notshot.Count);
		for (int i = 0; i < disk_notshot.Count; i++)
        {
            GameObject temp = disk_notshot[i].ufo;
            //飞碟飞出摄像机视野也没被打中
			// Debug.Log(temp.transform.position.x);
            if (temp.transform.position.x > 18 && temp.gameObject.activeSelf == true)
            {
                disk_factory.FreeDisk(disk_notshot[i]);
                disk_notshot.Remove(disk_notshot[i]);
                //玩家血量-1
                user_gui.ReduceBlood();
            }
        }
	}
    public void LoadResources()
    {
		Debug.Log("right here FirstController 3");
		disk_factory = Singleton<UFOFactor>.Instance;
        this_trival_ufos = disk_factory.GetDisk(round); 
    }


    public void Hit(Vector3 pos)
    {
		Debug.Log(pos);
		int index = -1;
        // Ray ray = Camera.main.ScreenPointToRay(pos);
		// RaycastHit[] hits;
		for(int i = 0;i < disk_notshot.Count;i ++){
			if(hited(pos,disk_notshot[i].ufo.transform.position)){
				Debug.Log(disk_notshot[i].ufo.transform.position);
				index = i;
				break;
			}
		}
		if(index == -1) return;
		
		curScore += disk_notshot[index].score;
		disk_factory.FreeDisk(disk_notshot[index]);
        disk_notshot.Remove(disk_notshot[index]);

        
    }
	
	public bool hited(Vector3 posScreen, Vector3 posObject){
		float xx = posScreen.x-(posObject.x * 22) - 512;
		float yy = posScreen.y-(posObject.y * 22) - 385;
		
		
		if(xx*xx + yy* yy < 1600 && yy*yy<200){
			Debug.Log(xx);
			Debug.Log(yy);
			return true;
		}
			
		// if(xx < 40 && xx > -40)
			// Debug.Log(xx);
		// if(yy < 10 && yy > -10)
			// Debug.Log(yy);
		return false;
	}
    //获得分数
    public int GetScore()
    {
        return curScore;
    }
    //重新开始
    public void ReStart()
    {
        game_over = false;
        playing_game = false;
        curScore = 0;
        round = 1;
        speed = 2f;
    }
    //设定游戏结束
    public void GameOver()
    {
        game_over = true;
		CancelInvoke("LoadResources");
    }

    public void BeginGame()
    {
        game_start = true;
    }
}
