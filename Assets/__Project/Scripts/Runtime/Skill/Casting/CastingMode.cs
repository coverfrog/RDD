using UnityEngine;

public enum CastingMode 
{
    Normal,
    /*
        단축키 -> 조준선 ->
            1. 좌클릭 -> 시전
            2. 우클릭 -> 취소
     */
    Quick,
    /*
        단축키 -> 커서위치 즉시 시전
     */
    QuickOnRelease,
    /*
        단축키 -> 조준선 ->
            1. 단축키 뗴기 -> 시전
            2. 우클릭 -> 취소
     */
}
