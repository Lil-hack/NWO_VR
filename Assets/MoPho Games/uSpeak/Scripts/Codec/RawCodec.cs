using UnityEngine;
using System.Collections;

using MoPhoGames.USpeak.Codec;

public class RawCodec : ICodec
{
	#region ICodec Members

	public byte[] Encode( short[] data )
	{
		byte[] result = new byte[ data.Length * sizeof( short ) ];
		System.Buffer.BlockCopy( data, 0, result, 0, result.Length );
		return result;
	}

	public short[] Decode( byte[] data )
	{
		short[] result = new short[ data.Length / sizeof( short ) ];
		System.Buffer.BlockCopy( data, 0, result, 0, data.Length );
		return result;
	}

	#endregion
}