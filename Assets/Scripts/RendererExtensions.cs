using UnityEngine;
 
public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		if (renderer == null) return false;
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}