/* Copyright (c) 2012 MoPho' Games
 * All Rights Reserved
 * 
 * Please see the included 'LICENSE.TXT' for usage rights
 * If this asset was downloaded from the Unity Asset Store,
 * you may instead refer to the Unity Asset Store Customer EULA
 * If the asset was NOT purchased or downloaded from the Unity
 * Asset Store and no such 'LICENSE.TXT' is present, you may
 * assume that the software has been pirated.
 * */

using UnityEngine;
using System.Collections;

using MoPhoGames.USpeak.Interface;

public class LocalUSpeakSender : MonoBehaviour, ISpeechDataHandler
{
    #region ISpeechDataHandler Members
    public byte[] data2;
    public int data2int;

    public void USpeakOnSerializeAudio( byte[] data )
	{
        data2 = data;

        USpeaker.Get( this ).ReceiveAudio( data );
	}

	public void USpeakInitializeSettings( int data )
	{
        data2int = data;

        USpeaker.Get( this ).InitializeSettings( data );
	}

	#endregion
}