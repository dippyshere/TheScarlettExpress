﻿#region

using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

#endregion

namespace Beautify.Universal
{
    class BeautifyOptCompiler : IPreprocessShaders
    {
        public const string PLAYER_PREF_KEYNAME = "BeautifyStripKeywordSet";

        public int callbackOrder
        {
            get { return 1; }
        }

        public void OnProcessShader(
            Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> shaderCompilerData)
        {
            try
            {
                if (shaderCompilerData == null)
                {
                    return;
                }

                if (!"Hidden/Universal Render Pipeline/UberPost".Equals(shader.name) &&
                    !"Hidden/Kronnect/Beautify".Equals(shader.name))
                {
                    return;
                }

                string strippedKeywords = PlayerPrefs.GetString(PLAYER_PREF_KEYNAME);
                if (string.IsNullOrEmpty(strippedKeywords))
                {
                    return;
                }

                for (int k = shaderCompilerData.Count - 1; k >= 0; k--)
                {
                    ShaderCompilerData data = shaderCompilerData[k];
                    ShaderKeyword[] keywords = data.shaderKeywordSet.GetShaderKeywords();
                    for (int s = 0; s < keywords.Length; s++)
                    {
                        ShaderKeyword keyword = keywords[s];
#if UNITY_2021_2_OR_NEWER
                        string keywordName = keyword.name;
#else
                        string keywordName;
                        if (ShaderKeyword.IsKeywordLocal(keyword))
                        {
                            keywordName = ShaderKeyword.GetKeywordName(shader, keyword);
                        }
                        else
                        {
                            keywordName = ShaderKeyword.GetGlobalKeywordName(keyword);
                        }
#endif
                        if (keywordName.Length > 0 && strippedKeywords.Contains(keywordName))
                        {
                            shaderCompilerData.RemoveAt(k);
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}