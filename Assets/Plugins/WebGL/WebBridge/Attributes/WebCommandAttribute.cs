// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebGlCommandAttribute.cs">
//   Copyright (c) 2022 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using JetBrains.Annotations;

namespace Supyrb.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	[MeansImplicitUse]
	public class WebCommandAttribute : Attribute
	{
		public string Description { get; set; }
	}
}