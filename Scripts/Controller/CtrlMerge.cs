using System;
using System.Collections.Generic;
using UnityEngine;

public class CtrlMerge
{
    public bool HasMerged;
    public bool HasMoved;

    public event EventHandler Finish;

    public void Merge(Direction dir)
    {
        //创建一个地图上所有方块的副本
        List<List<Cell>> Cells = Map.Instance.Cells;

        HasMerged = false;
        HasMoved = false;

        switch (dir)
        {
            //向左合并
            case Direction.Left:
                {
                    Debug.Log("向左合并");
                    for (int x = 0; x < 4; x++)
                    {
                        GameObject preEntity = null;
                        Cell preCell = null;

                        int index = 0;

                        for (int y = 0; y < 4; y++)
                        {
                            //单元格中无方块实体
                            if (Cells[x][y].AsociatedEntity == null)
                                continue;

                            //遍历到的第一个方块实体
                            if (preEntity == null)
                            {
                                preEntity = Cells[x][y].AsociatedEntity;
                                preCell = Cells[x][y];
                            }
                            //第二个
                            else
                            {
                                //和第一个方块的值相等
                                if(preEntity.GetComponent<Entity>().Value == Cells[x][y].AsociatedEntity.GetComponent<Entity>().Value)
                                {
                                    //合并，合并后得到的方块移动至左端空位
                                    GameObject newEntity = EntityFactory.Create(preEntity.GetComponent<Entity>().Value * 2);
                                    newEntity.transform.position = Cells[x][index].position;
                                    Cells[x][index].AsociatedEntity = newEntity;
                                    //更新得分
                                    CtrlScore.Score += preEntity.GetComponent<Entity>().Value * 2;
                                    //播放合并动画
                                    newEntity.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/merge");
                                    newEntity.GetComponent<Animator>().Play("merge");

                                    //销毁两个母方块实体
                                    UnityEngine.Object.Destroy(preEntity);
                                    UnityEngine.Object.Destroy(Cells[x][y].AsociatedEntity);
                                    
                                    //清空两个母方块所在单元格
                                    if (Cells[x][index] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                    }
                                    else
                                    {
                                        preCell = Cells[x][y];
                                    }
                                    index++;
                                    Cells[x][y].AsociatedEntity = null;

                                    preEntity = null;

                                    HasMerged = true;
                                }
                                //和第一个方块的值不等
                                else
                                {
                                    //上一个方块向左移动
                                    //preEntity.transform.position = Vector2.MoveTowards(preCell.position, Cells[x][index].position, 3.0f * Time.deltaTime);
                                    preEntity.transform.position = Cells[x][index].position;
                                    Cells[x][index].AsociatedEntity = preEntity;
                                    if ( Cells[x][index] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                        HasMoved = true;
                                    }
                                    //更新preEntity
                                    preEntity = Cells[x][y].AsociatedEntity;
                                    preCell = Cells[x][y];
                                    index++;
                                }
                            }
                        }
                        if (preEntity != null)
                        {
                            //方块向左移动
                            preEntity.transform.position = Cells[x][index].position;                            
                            Cells[x][index].AsociatedEntity = preEntity;
                            if (Cells[x][index++] != preCell)
                            {
                                preCell.AsociatedEntity = null;
                                HasMoved = true;
                            }
                        }
                        Debug.Log(index);
                    }
                }
                break;
            //向右合并
            case Direction.Right:
                {
                    Debug.Log("向右合并");
                    for (int x = 0; x < 4; x++)
                    {
                        GameObject preEntity = null;
                        Cell preCell = null;

                        int index = 0;

                        for (int y = 0; y < 4; y++)
                        {
                            //单元格中无方块实体
                            if (Cells[x][3-y].AsociatedEntity == null)
                                continue;

                            //遍历到的第一个方块实体
                            if (preEntity == null)
                            {
                                preEntity = Cells[x][3-y].AsociatedEntity;
                                preCell = Cells[x][3-y];
                            }
                            //第二个
                            else
                            {
                                //和第一个方块的值相等
                                if (preEntity.GetComponent<Entity>().Value == Cells[x][3-y].AsociatedEntity.GetComponent<Entity>().Value)
                                {
                                    //合并，合并后得到的方块移动至左端空位
                                    GameObject newEntity = EntityFactory.Create(preEntity.GetComponent<Entity>().Value * 2);
                                    newEntity.transform.position = Cells[x][3-index].position;
                                    Cells[x][3-index].AsociatedEntity = newEntity;
                                    //更新得分
                                    CtrlScore.Score += preEntity.GetComponent<Entity>().Value * 2;
                                    //播放合并动画
                                    newEntity.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/merge");
                                    newEntity.GetComponent<Animator>().Play("merge");

                                    //销毁两个母方块实体
                                    UnityEngine.Object.Destroy(preEntity);
                                    UnityEngine.Object.Destroy(Cells[x][3-y].AsociatedEntity);

                                    //清空两个母方块所在单元格
                                    if (Cells[x][3-index] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                    }
                                    else
                                    {
                                        preCell = Cells[x][3-y];
                                    }
                                    index++;
                                    Cells[x][3-y].AsociatedEntity = null;

                                    preEntity = null;

                                    HasMerged = true;
                                }
                                //和第一个方块的值不等
                                else
                                {
                                    //上一个方块向左移动
                                    //preEntity.transform.position = Vector2.MoveTowards(preCell.position, Cells[x][index].position, 3.0f * Time.deltaTime);
                                    preEntity.transform.position = Cells[x][3-index].position;
                                    Cells[x][3-index].AsociatedEntity = preEntity;
                                    if (Cells[x][3-index] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                        HasMoved = true;
                                    }
                                    //更新preEntity
                                    preEntity = Cells[x][3-y].AsociatedEntity;
                                    preCell = Cells[x][3-y];
                                    index++;
                                }
                            }
                        }
                        if (preEntity != null)
                        {
                            //方块向左移动
                            preEntity.transform.position = Cells[x][3-index].position;
                            Cells[x][3-index].AsociatedEntity = preEntity;
                            if (Cells[x][3-index++] != preCell)
                            {
                                preCell.AsociatedEntity = null;
                                HasMoved = true;
                            }
                        }
                        Debug.Log(index);
                    }
                }
                break;
            //向上合并
            case Direction.Up:
                {
                    Debug.Log("向上合并");
                    for (int y = 0; y < 4; y++)
                    {
                        GameObject preEntity = null;
                        Cell preCell = null;

                        int index = 0;

                        for (int x = 0; x < 4; x++)
                        {
                            //单元格中无方块实体
                            if (Cells[x][y].AsociatedEntity == null)
                                continue;

                            //遍历到的第一个方块实体
                            if (preEntity == null)
                            {
                                preEntity = Cells[x][y].AsociatedEntity;
                                preCell = Cells[x][y];
                            }
                            //第二个
                            else
                            {
                                //和第一个方块的值相等
                                if (preEntity.GetComponent<Entity>().Value == Cells[x][y].AsociatedEntity.GetComponent<Entity>().Value)
                                {
                                    //合并，合并后得到的方块移动至左端空位
                                    GameObject newEntity = EntityFactory.Create(preEntity.GetComponent<Entity>().Value * 2);
                                    newEntity.transform.position = Cells[index][y].position;
                                    Cells[index][y].AsociatedEntity = newEntity;
                                    //更新得分
                                    CtrlScore.Score += preEntity.GetComponent<Entity>().Value * 2;
                                    //播放合并动画
                                    newEntity.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/merge");
                                    newEntity.GetComponent<Animator>().Play("merge");

                                    //销毁两个母方块实体
                                    UnityEngine.Object.Destroy(preEntity);
                                    UnityEngine.Object.Destroy(Cells[x][y].AsociatedEntity);

                                    //清空两个母方块所在单元格
                                    if (Cells[index][y] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                    }
                                    else
                                    {
                                        preCell = Cells[x][y];
                                    }
                                    index++;
                                    Cells[x][y].AsociatedEntity = null;

                                    preEntity = null;

                                    HasMerged = true;                                   
                                }
                                //和第一个方块的值不等
                                else
                                {
                                    //上一个方块向左移动
                                    //preEntity.transform.position = Vector2.MoveTowards(preCell.position, Cells[x][index].position, 3.0f * Time.deltaTime);
                                    preEntity.transform.position = Cells[index][y].position;
                                    Cells[index][y].AsociatedEntity = preEntity;
                                    if (Cells[index][y] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                        HasMoved = true;
                                    }
                                    //更新preEntity
                                    preEntity = Cells[x][y].AsociatedEntity;
                                    preCell = Cells[x][y];
                                    index++;
                                }
                            }
                        }
                        if (preEntity != null)
                        {
                            //方块向左移动
                            preEntity.transform.position = Cells[index][y].position;
                            Cells[index][y].AsociatedEntity = preEntity;
                            if (Cells[index++][y] != preCell)
                            {
                                preCell.AsociatedEntity = null;
                                HasMoved = true;
                            }
                        }
                        Debug.Log(index);
                    }
                }
                break;
            //向下合并
            case Direction.Down:
                {
                    Debug.Log("向下合并");
                    for (int y = 0; y < 4; y++)
                    {
                        GameObject preEntity = null;
                        Cell preCell = null;

                        int index = 0;

                        for (int x = 0; x < 4; x++)
                        {
                            //单元格中无方块实体
                            if (Cells[3-x][y].AsociatedEntity == null)
                                continue;

                            //遍历到的第一个方块实体
                            if (preEntity == null)
                            {
                                preEntity = Cells[3-x][y].AsociatedEntity;
                                preCell = Cells[3-x][y];
                            }
                            //第二个
                            else
                            {
                                //和第一个方块的值相等
                                if (preEntity.GetComponent<Entity>().Value == Cells[3-x][y].AsociatedEntity.GetComponent<Entity>().Value)
                                {
                                    //合并，合并后得到的方块移动至左端空位
                                    GameObject newEntity = EntityFactory.Create(preEntity.GetComponent<Entity>().Value * 2);                                    
                                    newEntity.transform.position = Cells[3-index][y].position;
                                    Cells[3-index][y].AsociatedEntity = newEntity;
                                    //更新得分
                                    CtrlScore.Score += preEntity.GetComponent<Entity>().Value * 2;
                                    //播放合并动画
                                    newEntity.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animators/merge");
                                    newEntity.GetComponent<Animator>().Play("merge");

                                    //销毁两个母方块实体
                                    UnityEngine.Object.Destroy(preEntity);
                                    UnityEngine.Object.Destroy(Cells[3-x][y].AsociatedEntity);

                                    //清空两个母方块所在单元格
                                    if (Cells[3-index][y] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                    }
                                    else
                                    {
                                        preCell = Cells[3-x][y];
                                    }
                                    index++;
                                    Cells[3-x][y].AsociatedEntity = null;

                                    preEntity = null;

                                    HasMerged = true;                                    
                                }
                                //和第一个方块的值不等
                                else
                                {
                                    //上一个方块向左移动
                                    //preEntity.transform.position = Vector2.MoveTowards(preCell.position, Cells[x][index].position, 3.0f * Time.deltaTime);
                                    preEntity.transform.position = Cells[3-index][y].position;
                                    Cells[3-index][y].AsociatedEntity = preEntity;
                                    if (Cells[3-index][y] != preCell)
                                    {
                                        preCell.AsociatedEntity = null;
                                        HasMoved = true;
                                    }
                                    //更新preEntity
                                    preEntity = Cells[3-x][y].AsociatedEntity;
                                    preCell = Cells[3-x][y];
                                    index++;
                                }
                            }
                        }
                        if (preEntity != null)
                        {
                            //方块向左移动
                            preEntity.transform.position = Cells[3-index][y].position;
                            Cells[3-index][y].AsociatedEntity = preEntity;
                            if (Cells[3-index++][y] != preCell)
                            {
                                preCell.AsociatedEntity = null;
                                HasMoved = true;
                            }
                        }
                        Debug.Log(index);
                    }
                }
                break;
            default:
                break;
        }

        Debug.Log(CtrlScore.Score);
    }

    public void CheckFinish()
    {
        Finish(this,null);
    }
}
