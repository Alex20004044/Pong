using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MSFD
{
    [Obsolete("Use AS instead")]
    public static class AuxiliarySystem
    {
        public static Transform FindNearestTarget(Vector3 rootPoint, Transform[] targets)
        {
            Transform nearestTarget = null;
            float nearestDistSqr = float.PositiveInfinity;
            float distSqr;
            if (targets.Length == 1)
            {
                nearestTarget = targets[0];
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    distSqr = (rootPoint - targets[i].transform.position).sqrMagnitude;
                    if (distSqr < nearestDistSqr)
                    {
                        nearestTarget = targets[i].transform;
                        nearestDistSqr = distSqr;
                    }
                }
            }
            return nearestTarget;
        }
        public static Transform FindNearestTarget(Vector3 rootPoint, float searchRange, string[] targetTags)
        {
            Transform nearestTarget = null;
            float nearestDistSqr = float.PositiveInfinity;
            float distSqr;
            Collider[] colliders = Physics.OverlapSphere(AuxiliarySystem.ResetYAxis(rootPoint, GameValues.zeroLevel), searchRange, GameValues.ReturnUnitLayerMask());
            for (int i = 0; i < colliders.Length; i++)
            {
                if (MSFD.AuxiliarySystem.CompareTags(colliders[i].tag, targetTags))
                {
                    distSqr = (rootPoint - colliders[i].transform.position).sqrMagnitude;
                    if (distSqr < nearestDistSqr)
                    {
                        nearestTarget = colliders[i].transform;
                        nearestDistSqr = distSqr;
                    }
                }
            }
            return nearestTarget;
        }







        #region Rand Collection
        public static List<T> GetRandomDataList<T>(List<T> dataList, int count, bool isNeedToBeDifferent = true, bool isDebugLogExeptions = true)
        {
            if (dataList == null || dataList.Count == 0)
            {
                if (isDebugLogExeptions)
                    Debug.LogError("Trying to get randomElements from null or empty dataList");
                return null;
            }
            if (count == 0)
            {
                if (isDebugLogExeptions)
                {
                    Debug.LogError("Trying to get 0 randomElements");
                }
                return null;
            }
            if (count > dataList.Count && isNeedToBeDifferent)
            {
                if (isDebugLogExeptions)
                {
                    Debug.LogError("Trying to get incorrect count of elements. Count must be less then dataList.Count in DifferentData mode");
                }
                return null;
            }

            List<T> randomDataList = new List<T>();

            if (isNeedToBeDifferent)
            {
                List<ushort> availableIndexes = new List<ushort>();
                for (ushort i = 0; i < dataList.Count; i++)
                {
                    availableIndexes.Add(i);
                }
                for (int i = 0; i < count; i++)
                {
                    int ind = Random.Range(0, availableIndexes.Count);
                    randomDataList.Add(dataList[availableIndexes[ind]]);
                    availableIndexes.RemoveAt(ind);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    randomDataList.Add(GetRandomData<T>(dataList));
                }
            }
            return randomDataList;
        }
        public static List<T> GetRandomDataList<T>(List<T> dataList, int count, out List<int> indexes, bool isNeedToBeDifferent = true, bool isDebugLogExeptions = true)
        {
            indexes = new List<int>();
            if (dataList == null || dataList.Count == 0)
            {
                if (isDebugLogExeptions)
                    Debug.LogError("Trying to get randomElements from null or empty dataList");
                return null;
            }
            if (count == 0)
            {
                if (isDebugLogExeptions)
                    Debug.LogError("Trying to get 0 randomElements");

                return null;
            }
            if (count > dataList.Count && isNeedToBeDifferent)
            {
                if (isDebugLogExeptions)
                    Debug.LogError("Trying to get incorrect count of elements. Count must be less then dataList.Count in DifferentData mode");
                return null;
            }

            List<T> randomDataList = new List<T>();
            if (isNeedToBeDifferent)
            {
                List<ushort> availableIndexes = new List<ushort>();
                for (ushort i = 0; i < dataList.Count; i++)
                {
                    availableIndexes.Add(i);
                }
                for (int i = 0; i < count; i++)
                {
                    int ind = Random.Range(0, availableIndexes.Count);
                    randomDataList.Add(dataList[availableIndexes[ind]]);
                    indexes.Add(availableIndexes[ind]);
                    availableIndexes.RemoveAt(ind);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int randIndex = GetRandomIndex(dataList.Count);
                    randomDataList.Add(dataList[randIndex]);
                    indexes.Add(randIndex);
                }
            }
            return randomDataList;
        }
        public static List<T> GetRandomDataList<T>(List<T> dataList, ushort count, System.Predicate<T> searchCondition, bool isNeedToBeDifferent = true, bool isDebugLogExeptions = true)
        {
            List<T> correctDataList = new List<T>();

            foreach (T x in dataList)
            {
                if (searchCondition(x))
                {
                    correctDataList.Add(x);
                }
            }
            return GetRandomDataList<T>(correctDataList, count, isNeedToBeDifferent, isDebugLogExeptions);
        }
        public static T GetRandomData<T>(List<T> dataList)
        {
            int ind = Random.Range(0, dataList.Count);
            return dataList[ind];
        }

        public static int[] GetRandomIndexArray(int elementsCount, int randomIndexesCount, bool isNeedToBeDifferent = true)
        {
            if (randomIndexesCount == 0)
            {
                Debug.LogError("Trying to get 0 randomElements");
                return null;
            }
            if (randomIndexesCount > elementsCount && isNeedToBeDifferent)
            {
                Debug.LogError("Trying to get incorrect count of elements. Count must be less then dataList.Count in DifferentData mode");
                return null;
            }

            int[] randomIndexList = new int[randomIndexesCount];

            if (isNeedToBeDifferent)
            {
                List<ushort> availableIndexes = new List<ushort>();
                for (ushort i = 0; i < elementsCount; i++)
                {
                    availableIndexes.Add(i);
                }
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    int ind = Random.Range(0, availableIndexes.Count);
                    randomIndexList[i] = (availableIndexes[ind]);
                    availableIndexes.RemoveAt(ind);
                }
            }
            else
            {
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    randomIndexList[i] = (GetRandomIndex(randomIndexesCount));
                }
            }
            return randomIndexList;
        }
        public static List<int> GetRandomIndexList(int elementsCount, int randomIndexesCount, bool isNeedToBeDifferent = true)
        {
            if (randomIndexesCount == 0)
            {
                Debug.LogError("Trying to get 0 randomElements");
                return null;
            }
            if (randomIndexesCount > elementsCount && isNeedToBeDifferent)
            {
                Debug.LogError("Trying to get incorrect count of elements. Count must be less then dataList.Count in DifferentData mode");
                return null;
            }

            List<int> randomIndexList = new List<int>();

            if (isNeedToBeDifferent)
            {
                List<ushort> availableIndexes = new List<ushort>();
                for (ushort i = 0; i < elementsCount; i++)
                {
                    availableIndexes.Add(i);
                }
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    int ind = Random.Range(0, availableIndexes.Count);
                    randomIndexList.Add(availableIndexes[ind]);
                    availableIndexes.RemoveAt(ind);
                }
            }
            else
            {
                for (int i = 0; i < randomIndexesCount; i++)
                {
                    randomIndexList.Add(GetRandomIndex(randomIndexesCount));
                }
            }
            return randomIndexList;
        }
        public static int GetRandomIndex(int elementsCount)
        {
            int ind = Random.Range(0, elementsCount);
            return ind;
        }
        #endregion

        //AS>Utilities
        public static bool CompareTags(string[] tags, string[] targetTags)
        {
            bool result = false;
            for (int i = 0; i < tags.Length && !result; i++)
            {
                result = CompareTags(tags[i], targetTags);
            }
            return result;
        }
        public static bool CompareTags(string tag, string[] targetTags)
        {
            for (int i = 0; i < targetTags.Length; i++)
            {
                if (tag.Equals(targetTags[i]))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Use it to find interface in GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T FindComponent<T>(GameObject source)
        {
            T target;
            target = source.GetComponent<T>();
            if (target == null)
            {
                target = source.GetComponentInChildren<T>();
                if (target == null)
                {
                    Debug.LogError("Can't find component " + typeof(T).Name + " in " + source.name);
                }
            }
            return target;
        }
        public static T FindDesiredItem<T>(List<T> dataList, System.Func<T, T, bool> compareIsFirstItemBetter)
        {
            T best = dataList[0];

            for (int i = 1; i < dataList.Count; i++)
            {
                if (compareIsFirstItemBetter(dataList[i], best))
                {
                    best = dataList[i];
                }
            }
            return best;
        }

        public static bool RemoveDuplicateReferences<T>(List<T> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list.LastIndexOf(list[i]) != i)
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        public static bool RemoveNullReferences<T>(List<T> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        public static bool RemoveNullReferencesSO(List<ScriptableObject> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null || list[i].ToString() == "null")
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        public static List<V> ListConverter<T, V>(List<T> ts, System.Func<T, V> convertFunc)
        {
            List<V> vs = new List<V>(ts.Count);

            for (int i = 0; i < ts.Count; i++)
            {
                vs.Add(convertFunc(ts[i]));
            }
            return vs;
        }

        public static V[] ArrayConverter<T, V>(T[] ts, System.Func<T, V> convertFunc)
        {
            V[] vs = new V[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                vs[i] = convertFunc(ts[i]);
            }
            return vs;
        }

        //AS.Coordinate

        //Return angle 0-360 from OX to direction (Direction magnitude must be <= 1)
        public static float GetAngleFromDirection(Vector2 direction)
        {
            float angle = 0;
            float atg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (direction.x == 0)
            {
                if (direction.y == 1)
                {
                    angle = 90f;
                }
                else if (direction.y == -1)
                {
                    angle = 270f;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    angle = atg;
                }
                else
                {
                    angle = atg + 360f;
                }
            }
            return angle;
        }

        public static Vector3 ResetYAxis(Vector3 vector, float y = 0)
        {
            return new Vector3(vector.x, y, vector.z);
        }
        public static Vector2 PackVector3ToVector2(Vector3 vector, bool isPackZ = true)
        {
            if (isPackZ)
            {
                return new Vector2(vector.x, vector.z);
            }
            else
            {
                return new Vector2(vector.x, vector.y);
            }
        }

        //---AS.Rand
        public static Vector3 RandomPointInBounds(Bounds bounds)
        {
            return RandomPointInBounds(bounds.center, bounds.size);
        }
        public static Vector3 RandomPointInBounds(Vector3 center, Vector3 size)
        {
            Vector3 randomPoint = Vector3.zero;

            randomPoint.x = Random.Range(-size.x / 2, size.x / 2);
            randomPoint.y = Random.Range(-size.y / 2, size.y / 2);
            randomPoint.z = Random.Range(-size.z / 2, size.z / 2);
            return randomPoint + center;
        }
        public static Vector3 RandomPointInSphere(Vector3 center, float radius)
        {
            return center + Random.insideUnitSphere * radius;
        }
        public static Quaternion GetRandomRotationYAxis()
        {
            return Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
        }
    }
    
}