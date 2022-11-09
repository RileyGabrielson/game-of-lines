using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineView : MonoBehaviour
{
  public Transform sprite;
  public SpriteRenderer lineSprite;
  public float fadeSpeed;

  private Vector3 target;
  private string id;

  private void Update() {
    this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, 3 * Time.deltaTime);

    Color color = this.lineSprite.color;
    color.a = color.a + (Time.deltaTime * this.fadeSpeed);
    this.lineSprite.color = color;
  }

  public void SetId(string id) {
    this.id = id;
  }

  public void SetDirection(MovingLine.Direction direction) {
    if(direction == MovingLine.Direction.LEFT || direction == MovingLine.Direction.RIGHT) {
      sprite.transform.localScale = new Vector3(0.1f, 1f, 1f);
    }
    else {
      sprite.transform.localScale = new Vector3(1f, 0.1f, 1f);
    }
  }

  public void UpdateTarget(Lines lines) {
    Position position = lines.GetLineById(this.id).getCurrentPosition();
    Vector3 newTarget = new Vector3(position.x, position.y, 0f);
    this.target = newTarget;
  }

  public void UpdateTarget(Vector3 position) {
    this.target = position;
  }
}
