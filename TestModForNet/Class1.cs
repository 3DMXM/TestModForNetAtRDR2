using System;
using System.Windows.Forms;
using RDR2;
using RDR2.UI;
using RDR2.Native;
using RDR2.Math;

namespace TestModForNet
{
    public class Main : Script
    {
        public Main()   //入口函数
        {
            //KeyDown是ScriptHookRDRNetAPI中自带的函数，用于监听按键的按下事件
            //这句的意思是如果有按键按下则执行函数“OnKeyDown”
            KeyDown += OnKeyDown;

            //Tick是ScriptHookRDRNetAPI中自带的函数，用于勾住游戏
            //这句的意思是将函数“OnTick”注入到游戏中，并且每过“Interval”毫秒进行检测一次
            Tick += OnTick;

            //Interval是检测间隔，单位毫秒
            Interval = 1;
        }
        bool ModOn = false; //给Mod设置一个开关
        private void OnTick(object sender, EventArgs e)
        {
            Player player = Game.Player;    //获取玩家
            var IsDead = player.IsDead;
            if (ModOn)  //如果
            {
                //Function.Call是ScriptHookRDRNetAPI中的函数，用于执行动作
                //Hash.SET_SUPER_JUMP_THIS_FRAME中的“SET_SUPER_JUMP_THIS_FRAME”是 Script Hook RDR2 SDK 中的内容
                //具体赋值内容可以参考 Script Hook RDR2 SDK 的“natives.h”文件
                // SET_SUPER_JUMP_THIS_FRAME(Player player)
                Function.Call(Hash.SET_SUPER_JUMP_THIS_FRAME, player, true);
            }
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)    //判断按下的键是否是“C”键，定义启动按键
            {
                ModOn = !ModOn; //给Mod开关赋值一个反值，如果开就关，如果关就开
            }
            if (e.KeyCode == Keys.Up)
            {
                changeTime(1);
            }
            if (e.KeyCode == Keys.Down)
            {
                changeTime(-1);
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <param name="time"></param>
        private void changeTime(int time)
        {
            Function.Call(Hash.ADD_TO_CLOCK_TIME, time, 0, 0, true);
        }

    }
}
