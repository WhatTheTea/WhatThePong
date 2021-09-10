using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static List<Goal> Instances { get; private set; }

    #region fields
    [SerializeField]
#pragma warning disable IDE0044 // �������� ����������� ������ ��� ������
    private Collider2D hitbox;
#pragma warning restore IDE0044 // �������� ����������� ������ ��� ������
    #endregion
    #region properties
    public Collider2D HitBox => hitbox;
    public string Name => name;
    public string Tag => tag;
    #endregion

#pragma warning disable IDE0051 // ������� �������������� �������� �����
    private void Start() => Instances.Add(this);
    private void OnDestroy() => Instances.Remove(this);
#pragma warning restore IDE0051 // ������� �������������� �������� �����
}
