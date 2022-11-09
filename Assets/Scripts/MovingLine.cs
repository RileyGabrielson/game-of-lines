using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLine : Line
{
  private Position position;
  private Line.Direction direction;
  private Position stagedPosition;

  public MovingLine(Position position, Line.Direction direction) {
    this.position = position;
    this.direction = direction;
  }

  public override Position getNextPosition(Lines lines) {
    bool isImpactingStationaryLine = this.isImpactingStationaryLine(lines);
    if(isImpactingStationaryLine) {
      return this.travelDirection(this.reverseDirection(this.direction));
    }
    return this.travelDirection(this.direction);
  }

  public override void stageNextMove(Lines lines) {
    this.stagedPosition = this.getNextPosition(lines);
  }

  public override void moveToStagedPosition(Lines lines) {
    if(this.isImpactingStationaryLine(lines)) this.direction = this.reverseDirection(this.direction);
    this.position = stagedPosition;
  }

  public override bool canReproduce(Lines lines) {
    return this.isImpactingMovingLine(lines)
      && (this.direction == Direction.RIGHT || this.direction == Direction.DOWN) 
      && !this.wouldMakeDuplicateLine(lines);
  }

  public override Line reproduce() {
    return new MovingLine(new Position(this.position.x, this.position.y), this.orthogonalDirection(this.direction));
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

  private bool isImpactingMovingLine(Lines lines) {
    int numImpacts = 0;
    List<Line> linesAtPosition = lines.GetLinesAtPosition(this.getCurrentPosition());

    linesAtPosition.ForEach(line => {
      if (line is MovingLine) {
        numImpacts += 1;
      }
    });

    return numImpacts > 1;
  }

  private bool wouldMakeDuplicateLine(Lines lines) {
    Direction orthogonalDirection = this.orthogonalDirection(this.direction);
    List<Line> linesAtPosition = lines.GetLinesAtPosition(this.getCurrentPosition());
    bool isUnique = true;
    
    linesAtPosition.ForEach(line => {
      if (line.getDirection() == orthogonalDirection && line is MovingLine) {
        isUnique = false;
      }
    });

    return !isUnique;
  }

  private bool isImpactingStationaryLine(Lines lines) {
    List<Line> linesAtPosition = lines.GetLinesAtPosition(this.getCurrentPosition());
    bool multipleLines = linesAtPosition.Count > 1;
    if(multipleLines) {
      return this.collidingDirections(linesAtPosition[0].getDirection(), linesAtPosition[1].getDirection());
    }
    return false;
  }

  private bool collidingDirections(Direction direction1, Direction direction2) {
    if(direction1 == Direction.LEFT || direction1 == Direction.RIGHT) {
      return direction2 == Direction.LEFT || direction2 == Direction.RIGHT;
    }
    else {
      return direction2 == Direction.UP || direction2 == Direction.DOWN;
    }
  }

  private Position travelDirection(Direction direction) {
    if(direction == Direction.UP) return new Position(this.position.x, this.position.y + 1);
    else if(direction == Direction.DOWN) return new Position(this.position.x, this.position.y - 1);
    else if(direction == Direction.LEFT) return new Position(this.position.x - 1, this.position.y);
    else return new Position(this.position.x + 1, this.position.y);
  }

  private Direction reverseDirection(Direction direction) {
    if(direction == Direction.UP) return Direction.DOWN;
    else if(direction == Direction.DOWN) return Direction.UP;
    else if(direction == Direction.LEFT) return Direction.RIGHT;
    else return Direction.LEFT;
  }

  private Direction orthogonalDirection(Direction direction) {
    if(direction == Direction.UP || direction == Direction.DOWN) return Direction.RIGHT;
    else return Direction.DOWN;
  }

}
