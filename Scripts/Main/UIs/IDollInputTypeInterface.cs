using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDollInputTypeInterface {

    //人形の種類によって入力回数取得の共通化
    int TypeOrderCount(Matryoshka.DollInput_Type dollInputType);
}
