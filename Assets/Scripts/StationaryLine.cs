using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryLine : Line
{
  private Position position;
  private Line.Direction direction;

  public StationaryLine(Position position, Line.Direction direction) {
    this.position = position;
    this.direction = direction;
  }

  public override Position getNextPosition(Lines lines) {
    return this.position;
  }

  public override void stageNextMove(Lines lines) {}

  public override void moveToStagedPosition(Lines lines) {}

  public override bool canReproduce(Lines lines) {
    return false;
  }

  public override Line reproduce() {
    return null;
  }

  public override bool isDead() {
    return false;
  }

  public override Position getCurrentPosition() {
    return this.position;
  }

  public override Direction getDirection() {
    return this.direction;
  }
}
