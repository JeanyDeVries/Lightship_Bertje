using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static Niantic.ARDKExamples.WayspotAnchors.WayspotAnchorDataUtility;
using static Niantic.ARDKExamples.WayspotAnchors.WayspotAnchorExampleManager;

namespace Niantic.ARDKExamples.WayspotAnchors
{
    public class Database : MonoBehaviour
    {
        private const string URL = "https://armaps.grote.beer/Niantic/";
        private static Database _instance;

        /// <summary>
        /// The singleton instance referring to the class
        /// </summary>
        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<Database>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// returns the whole json with all anchors from the given map name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="successFull"></param>
        /// <param name="notSuccessFull"></param>
        /// <returns></returns>
        public IEnumerator GetFullData(string id, Action<WayspotAnchorsData> successFull, Action notSuccessFull)
        {
            string fullUrl = URL + "?id=waySpotAnchor_" + id;
            print(fullUrl);
            var request = UnityWebRequest.Get(fullUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success && request.result != UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("before error");
                print(request.downloadHandler.text);
                var success = TryConvert<WayspotAnchorsData>(request.downloadHandler.text, out var data);

                if (!success)
                {
                    Debug.Log("Could not convert data");
                    notSuccessFull.Invoke();
                    yield return null;
                }

                successFull.Invoke(data);
            }
            else
            {
                notSuccessFull?.Invoke();
            }
        }

        public IEnumerator SaveData(string data, Action successFull, Action notSuccessFull)
        {
            string jsonData = data;
            string fullUrl = URL + "?id=waySpotAnchor_" + data;

            using UnityWebRequest request = UnityWebRequest.Put(URL, jsonData);
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("accept", "application/json");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success && request.result != UnityWebRequest.Result.ConnectionError)
            {
                successFull?.Invoke();
            }
            else
            {
                notSuccessFull?.Invoke();
            }
        }

        public bool TryConvert<T>(string json, out T result)
        {
            result = default(T);
            try
            {
                result = JsonUtility.FromJson<T>(json);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [Serializable]
        public class PropInfo
        {
            public string url = string.Empty;
            public Vector2Int wantedTextureSize = Vector2Int.zero;
            public bool isBillBoard = false;
            public bool isDepthMapping = false;
            public float[] position = new float[3];
            public float[] rotation = new float[4];
            public float[] scale = new float[3];
        }

        [Serializable]
        public class DenseInfo
        {
            public float[] position = new float[3];
            public float[] rotation = new float[4];
            public float[] scale = new float[3];
        }
    }
}
