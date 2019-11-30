using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum Status
{
    None = 0,
    Wait,
    Playing,
    End,
}

public enum GameStatus
{
    None = 0,
    Normal = 1,
    Kill = 2,
    End

}
public enum EBuffType
{
    Blooding,
    Stabbed, // 날카로운것에 찔리다
    Die,
    NeckInjury, //목 주변 상처
    HandInjury, //손 주변 상처
    BodyInjury, //상체 상처
    RigorMotis, //사후경직
    HeadBoneBreak, //두개골 부서짐
    ChinBreak, //턱 부서짐
    FingerBreak, //손가락 부러짐
    ArmBreak, //팔 부러짐
    RednessOfFaceSkin // 얼굴 붉어짐


}