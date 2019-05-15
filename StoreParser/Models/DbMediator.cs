using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreParser.Models
{
    public class DbMediator
    {
        private GoodsDatabase db = new GoodsDatabase();

        public List<Good> GetGoodsByRef(string reference)
        {
            List<Good> result = new List<Good>();
            try
            {
                result = db.Goods.Where(good => good.Link == reference).ToList();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public int InsertGood(Good good)
        {
            int result = 0;
            try
            {
                db.Goods.Add(good);
                db.SaveChanges();
                result = good.Id;
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public int InsertImage(Image img)
        {
            int result = 0;
            try
            {
                db.Images.Add(img);
                db.SaveChanges();
                result = img.Id;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public int InsertPrice(Price price)
        {
            int result = 0;
            try
            {
                db.Prices.Add(price);
                db.SaveChanges();
                result = price.Id;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public int GetGoodsCount()
        {
            int result = 0;
            try
            {
                result = (from good in db.Goods
                          select good.Id).Count();
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public int[] GetGoodIds(int topCount)
        {
            int[] result = null;
            try
            {
                if (topCount > 0)
                {
                    result = (from good in db.Goods
                              select good.Id).Take(topCount).ToArray();
                }
                else
                {
                    result = (from good in db.Goods
                              select good.Id).ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public List<Price> GetGoodPrice(int goodId, int count)
        {
            List<Price> result = null;
            try
            {
                var query = from price in db.Prices
                            where price.GoodId == goodId
                            select price;
                if (count > 0)
                {
                    query = query.OrderByDescending(price => price.Id).Take(count);
                    result = query.ToList();
                }
                else
                {
                    result = query.ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public List<Image> GetGoodImage(int goodId, int count)
        {
            List<Image> result = null;
            try
            {
                var query = from img in db.Images
                            where img.GoodId == goodId
                            select img;
                if (count > 0)
                {
                    query = query.OrderByDescending(img => img.Id).Take(count);
                    result = query.ToList();
                }
                else
                {
                    result = query.ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            return result;
        }

        public ModelGood GetModelGood(int id)
        {
            ModelGood result = null;
            try
            {
                if (id > 0)
                {
                    result = (from good in db.Goods
                              where good.Id == id
                              select new ModelGood()
                              {
                                  Id = good.Id,
                                  Title = good.Title,
                                  Description = good.Description
                              }).First();
                }
                else
                {
                    result = (from good in db.Goods
                              select new ModelGood()
                              {
                                  Id = good.Id,
                                  Title = good.Title,
                                  Description = good.Description
                              }).First();
                }
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message);
            }
            if(result != null)
            {
                result.Price = GetGoodPrice(result.Id, 1).First().Cost;
                result.Image = GetGoodImage(result.Id, 1).First().Img;
                result.GoodIds = GetGoodIds(0);
            }
            return result;
        }
    }
}