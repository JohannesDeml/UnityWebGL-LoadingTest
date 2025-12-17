// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildScript.cs">
//   Copyright (c) 2023 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace UnityBuilderAction
{
	/// <summary>
	/// Code optimization settings for the build
	/// See also <see href="https://forum.unity.com/threads/webgl-build-code-optimization-option.1058441/#post-9818385" />
	/// </summary>
	enum CodeOptimizationWebGL
	{
		BuildTimes,
		RuntimeSpeed,
		RuntimeSpeedLTO,
		DiskSize,
		DiskSizeLTO,
	}
}