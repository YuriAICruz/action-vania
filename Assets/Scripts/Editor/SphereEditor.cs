using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sphere))]
public class SphereEditor : Editor
{
    private Sphere _self;

    private void Awake()
    {
        _self = target as Sphere;
    }

    private void OnSceneGUI()
    {
        var points = new Vector3[_self.Resolution * 3].ToList();

        var center = _self.transform.position;

        if (_self.Resolution <= 3)
        {
            _self.Resolution = 4;
            return;
        }

        for (int i = 0; i < _self.Resolution; i++)
        {
            var dirX = new Vector3(0, Mathf.Sin(i / (float) _self.Resolution * Mathf.PI * 2), Mathf.Cos(i / (float) _self.Resolution * Mathf.PI * 2));

            points[i * 3] = center + dirX * _self.Radius;

            var dirY = new Vector3(Mathf.Sin(i / (float) _self.Resolution * Mathf.PI * 2), 0, Mathf.Cos(i / (float) _self.Resolution * Mathf.PI * 2));

            points[i * 3 + 1] = center + dirY * _self.Radius;

            var dirZ = new Vector3(Mathf.Sin(i / (float) _self.Resolution * Mathf.PI * 2), Mathf.Cos(i / (float) _self.Resolution * Mathf.PI * 2), 0);

            points[i * 3 + 2] = center + dirZ * _self.Radius;
        }

        _self.Size = (points[0] - points[3]).magnitude;

        for (int i = 0, n = _self.Resolution * 3; i < _self.Resolution; i++)
        {
            var p = -points[i * 3] + points[(i * 3 + 3) % n];
            var d = Vector3.Cross(-points[i*3]+center, p);
            Debug.DrawRay(points[i*3], d* _self.Size*0.5f);
            Debug.DrawRay(points[i*3], -d* _self.Size*0.5f);
            
            Debug.DrawLine(points[i * 3], points[(i * 3 + 3) % n]);
            Debug.DrawLine(points[i * 3 + 1], points[(i * 3 + 4) % n]);
            Debug.DrawLine(points[i * 3 + 2], points[(i * 3 + 5) % n]);
//            Debug.DrawRay(points[i], Vector3.Scale(center, Vector3.right) * _self.Size);
//            Debug.DrawRay(points[i], Vector3.Scale(center, Vector3.forward) * _self.Size);
//            Debug.DrawRay(points[i], Vector3.Scale(center, Vector3.up) * _self.Size);
        }
    }
}