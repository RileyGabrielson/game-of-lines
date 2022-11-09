using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Line : System.Object
{
  public enum Direction {
    LEFT,
    RIGHT,
    UP,
    DOWN
  }

  public abstract Position getNextPosition(Lines lines);
  public abstract bool canReproduce(Lines lines);
  public abstract Line reproduce();
  public abstract bool isDead();
  public abstract Position getCurrentPosition();

  public abstract void stageNextMove(Lines lines);
  public abstract void moveToStagedPosition(Lines lines);
  public abstract Direction getDirection();
}
