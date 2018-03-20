﻿using UnityEngine;
using ur = UnityEngine.Rendering;

namespace TESUnity
{
    /// <summary>
    /// A material that uses the legacy Bumped Diffuse Shader.
    /// </summary>
    public class MWLightweightMaterial : MWBaseMaterial
    {
        public MWLightweightMaterial(TextureManager textureManager) : base(textureManager) { }

        public override Material BuildMaterialFromProperties(MWMaterialProps mp)
        {
            Material material;

            //check if the material is already cached
            if (!m_existingMaterials.TryGetValue(mp, out material))
            {
                //otherwise create a new material and cache it
                if (mp.alphaBlended)
                    material = BuildMaterialBlended(mp.srcBlendMode, mp.dstBlendMode);
                else if (mp.alphaTest)
                    material = BuildMaterialTested(mp.alphaCutoff);
                else
                    material = BuildMaterial();

                if (mp.textures.mainFilePath != null)
                {
                    material.mainTexture = m_textureManager.LoadTexture(mp.textures.mainFilePath);

                    if (TESUnity.instance.generateNormalMap)
                        material.SetTexture("_BumpMap", GenerateNormalMap((Texture2D)material.mainTexture, TESUnity.instance.normalGeneratorIntensity));
                }

                if (mp.textures.bumpFilePath != null)
                    material.SetTexture("_BumpMap", m_textureManager.LoadTexture(mp.textures.bumpFilePath));

                m_existingMaterials[mp] = material;
            }
            return material;
        }

        public override Material BuildMaterial()
        {
            if (TESUnity.instance.materialType == TESUnity.MWMaterialType.PBR)
                return new Material(Shader.Find("LightweightPipeline/Standard (Physically Based)"));
            else
                return new Material(Shader.Find("LightweightPipeline/Standard (Simple Lighting)"));
        }

        public override Material BuildMaterialBlended(ur.BlendMode sourceBlendMode, ur.BlendMode destinationBlendMode)
        {
            Material material = BuildMaterial();
            //material.SetInt("_SrcBlend", (int)sourceBlendMode);
            //material.SetInt("_DstBlend", (int)destinationBlendMode);
            return material;
        }

        public override Material BuildMaterialTested(float cutoff = 0.5f)
        {
            Material material = BuildMaterial();
            //material.SetFloat("_AlphaCutoff", cutoff);
            return material;
        }
    }
}