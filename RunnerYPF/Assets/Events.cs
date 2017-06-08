using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action CreatorReset = delegate { };
	public static System.Action OnAddNewLine = delegate { };
	public static System.Action Jump = delegate { };
	public static System.Action<int> SpeedChange = delegate { };
}
