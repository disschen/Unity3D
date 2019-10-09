﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class UFOFactor : MonoBehaviour
{
    private List<UFO> used = new List<UFO>();   //正在被使用的飞碟列表
    private List<UFO> free = new List<UFO>();   //空闲的飞碟列表

    public List <UFO> GetDisk(int round)
    {
		Debug.Log("right here UFOFactor 1");
        int choice = 0;
        int scope1 = 1, scope2 = 2, scope3 = 3;           //随机的范围
        float start_y = -10f;                             //刚实例化时的飞碟的竖直位置
		int ufo_num = Random.Range(1, round + 1);
		int counts = 0;
		// 清空used
		used = new List<UFO>();
        // 根据回合，选择要飞出的UFO的种类
		
		for(int i=0;i<free.Count;i++) {
			Debug.Log("not empty");
			int type = Random.Range(1, round+1);
			if(free[i].type == type) {
				used.Add(free[i]);
				free.Remove(free[i]);
				i --;
				counts ++;
				if(counts == ufo_num) 
					break;
			}
		}
		
		if(counts < ufo_num){
			for (int i = 0;i < ufo_num-counts;i ++){
				int type = Random.Range(1, round);
				UFO new_ufo = new UFO(type);
				used.Add(new_ufo);
			}
		}
		return used;
    }

    // 回收飞碟
    public void FreeDisk(UFO aUFO)
    {
		aUFO.ufo.SetActive(false);
		aUFO.ufo.transform.position = Vector3.zero;
		free.Add(aUFO);
    }
}
