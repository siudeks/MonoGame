﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Microsoft.Xna.Framework.Graphics
{

    /// <summary>
    /// Represents a mesh that is part of a Model.
    /// </summary>
    public sealed class ModelMesh
	{
		private GraphicsDevice graphicsDevice;

		public ModelMesh(GraphicsDevice graphicsDevice, System.Collections.Generic.List<ModelMeshPart> parts)
		{
			// TODO: Complete member initialization
			this.graphicsDevice = graphicsDevice;
			
			MeshParts = new ModelMeshPartCollection(parts);
			
			for (int i = 0; i < parts.Count; i++) {
				parts[i].parent = this;
			}
			
			Effects = new ModelEffectCollection();
		}

        /// <summary>
        /// Gets the BoundingSphere that contains this mesh.
        /// </summary>
        public BoundingSphere BoundingSphere { get; set; }

        /// <summary>
        /// Gets a collection of effects associated with this mesh.
        /// </summary>
		public ModelEffectCollection Effects { get; internal set; }

        /// <summary>
        /// Gets the ModelMeshPart objects that make up this mesh. Each part of a mesh
        /// is composed of a set of primitives that share the same material.
        /// </summary>
        public ModelMeshPartCollection MeshParts { get; set; }

        /// <summary>
        /// Gets the name of this mesh.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Gets the parent bone for this mesh. The parent bone of a mesh contains a
        /// transformation matrix that describes how the mesh is located relative to
        /// any parent meshes in a model.
        /// </summary>
        public ModelBone ParentBone { get; set; }

        /// <summary>
        /// Gets or sets an object identifying this mesh.
        /// </summary>
		public object Tag { get; set; }

        /// <summary>
        /// Draws all of the ModelMeshPart objects in this mesh, using their current
        /// Effect settings.
        /// </summary>        
        public void Draw()
		{	
			for(int i = 0; i < MeshParts.Count; i++)
			{
				var part = MeshParts[i];
				var effect = part.Effect;
				
				if (part.PrimitiveCount > 0)
				{
                    this.graphicsDevice.SetVertexBuffer(part.VertexBuffer);
                    this.graphicsDevice.Indices = part.IndexBuffer;
                    
                    for (int j = 0; j < effect.CurrentTechnique.Passes.Count; j++)
                    {
						effect.CurrentTechnique.Passes[j].Apply ();
						graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.StartIndex, part.PrimitiveCount);
					}
				}
			}
		}
	}
}
