package crc64d0795dff680a4bab;


public class FireBaseTokenRefreshService
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("Doppler.Droid.FireBaseService.FireBaseTokenRefreshService, Doppler.Android", FireBaseTokenRefreshService.class, __md_methods);
	}


	public FireBaseTokenRefreshService ()
	{
		super ();
		if (getClass () == FireBaseTokenRefreshService.class)
			mono.android.TypeManager.Activate ("Doppler.Droid.FireBaseService.FireBaseTokenRefreshService, Doppler.Android", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
