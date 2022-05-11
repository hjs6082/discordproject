using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {
    public FlipMode mode;
    public float pageFlipTime = 1;
    public float timeBetweenPages = 1;
    public float delayBeforeStarting = 0;
    public bool autoStartFlip=true;
    public Book controledBook;
    public int animationFramesCount = 40;
    bool isFlipping = false;

    void Start ()
    {
       if (!controledBook) { controledBook = GetComponent<Book>(); }
       //if (autoStartFlip)  { StartFlipping(); }

       controledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
	}

    void PageFlipped()
    {
        isFlipping = false;
    }

	public void StartFlipping()
    {
        StartCoroutine(FlipToEnd());
    }

    public void FlipRightPage()
    {
        if (isFlipping) return;

        if (controledBook.currentPage >= controledBook.TotalPageCount) return;

        isFlipping = true;

        float frameTime = pageFlipTime / animationFramesCount;
        float xc = (controledBook.EndBottomRight.x + controledBook.EndBottomLeft.x) / 2;
        float xl = ((controledBook.EndBottomRight.x - controledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  controledBook.Height * 0.5f;
        float h = Mathf.Abs(controledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / animationFramesCount;

        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }

    public void FlipLeftPage()
    {
        if (isFlipping) return;
        if (controledBook.currentPage <= 0) return;

        isFlipping = true;

        float frameTime = pageFlipTime / animationFramesCount;
        float xc = (controledBook.EndBottomRight.x + controledBook.EndBottomLeft.x) / 2;
        float xl = ((controledBook.EndBottomRight.x - controledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  controledBook.Height * 0.5f;
        float h = Mathf.Abs(controledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / animationFramesCount;

        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
    }

    IEnumerator FlipToEnd()
    {
        yield return new WaitForSeconds(delayBeforeStarting);

        float frameTime = pageFlipTime / animationFramesCount;
        float xc = (controledBook.EndBottomRight.x + controledBook.EndBottomLeft.x) / 2;
        float xl = ((controledBook.EndBottomRight.x - controledBook.EndBottomLeft.x) / 2)*0.9f;
        //float h =  controledBook.Height * 0.5f;
        float h = Mathf.Abs(controledBook.EndBottomRight.y)*0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl)*2 / animationFramesCount;

        switch (mode)
        {
            case FlipMode.RightToLeft:
            {
                while (controledBook.currentPage < controledBook.TotalPageCount)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(timeBetweenPages);
                }
            }
            break;
            case FlipMode.LeftToRight:
            {
                while (controledBook.currentPage > 0)
                {
                    StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(timeBetweenPages);
                }
            }
            break;
        }
    }

    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        controledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        
        for (int i = 0; i < animationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            controledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        controledBook.ReleasePage();
    }

    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        controledBook.DragLeftPageToPoint(new Vector3(x, y, 0));

        for (int i = 0; i < animationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            controledBook.UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x += dx;
        }
        controledBook.ReleasePage();
    }
}
