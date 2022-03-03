package crc64bc9e702cdb7b3a22;


public class GroupedListViewAdapter
	extends crc64bc9e702cdb7b3a22.ListViewAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Microsoft.Maui.Controls.Handlers.Compatibility.GroupedListViewAdapter, Microsoft.Maui.Controls.Compatibility", GroupedListViewAdapter.class, __md_methods);
	}


	public GroupedListViewAdapter ()
	{
		super ();
		if (getClass () == GroupedListViewAdapter.class)
			mono.android.TypeManager.Activate ("Microsoft.Maui.Controls.Handlers.Compatibility.GroupedListViewAdapter, Microsoft.Maui.Controls.Compatibility", "", this, new java.lang.Object[] {  });
	}

	public GroupedListViewAdapter (android.content.Context p0)
	{
		super ();
		if (getClass () == GroupedListViewAdapter.class)
			mono.android.TypeManager.Activate ("Microsoft.Maui.Controls.Handlers.Compatibility.GroupedListViewAdapter, Microsoft.Maui.Controls.Compatibility", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
