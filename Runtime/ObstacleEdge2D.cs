﻿using Nebukam.Utils;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

namespace Nebukam.Beacon.ORCA
{

    [AddComponentMenu("Nebukam/Beacon/ORCA/Obstacle Edge 2D")]
    public class ObstacleEdge2D : ObstacleConverter<EdgeCollider2D>
    {

        protected override void BuildObstacles()
        {

            SetObstacleCount(1);

            Vector2[] points = colliderComponent.points;
            Vector2 offset = colliderComponent.offset;
            float3 pos = transform.position;
            quaternion rot = transform.rotation;
            int count = points.Length;

            Nebukam.ORCA.Obstacle o = SetObstacleLength(0, count);
            o.edge = true;

            float3 Project(Vector2 pt)
            {
                float3 proj = Maths.RotateAroundPivot(float3(pt.x + offset.x, pt.y + offset.y, pos.z), float3(false), rot);
                proj.x += pos.x; proj.y += pos.y; proj.z = pos.z;
                return proj;
            }

            for (int i = 0; i < count; i++)
                o[i].pos = Project(points[i]);

        }

#if UNITY_EDITOR

        #region debug draw

        protected override void DrawObstaclePreview(EdgeCollider2D component, Color col)
        {
            EdgeCollider2D CC = colliderComponent;
            if (CC == null) { CC = GetComponent<EdgeCollider2D>(); }
            if (CC == null) { return; }

            Vector2[] points;
            Vector2 offset = CC.offset;
            float3 pos = transform.position;
            quaternion rot = transform.rotation;

            points = CC.points;

            float3 Project(Vector2 pt)
            {
                float3 proj = Maths.RotateAroundPivot(float3(pt.x + offset.x, pt.y + offset.y, pos.z), float3(false), rot);
                proj.x += pos.x; proj.y += pos.y; proj.z = pos.z;
                return proj;
            }

            for (int i = 1, count = points.Length; i < count; i++)
                DrawSegment(Project(points[i - 1]), Project(points[i]), col);
        }
    
        #endregion

#endif

    }
}