
using AutoMapper;
using Cms.Data.DAL.Context;
using Cms.Data.DAL.Context.Models;
using Cms.Data.DAL.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Data.DAL.Core
{
    class StockDatastore : IStockDatastore
    {
        private readonly IMapper _mapper;
        private readonly CmsStockManagmentContext _context;
        public StockDatastore(CmsStockManagmentContext context,
                             IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Types.StockItem>> GetAsync(Types.PaginationFilter filter = null)
        {
            try
            {
                List<StockItem> results = null;
                if (filter == null)
                {
                    filter = new Types.PaginationFilter { PageNumber = 1, PageSize = 1000 };
                }
                var recordsToSkip = (filter.PageNumber - 1) * filter.PageSize;
                results = await _context.StockItem.Include(x => x.Image)
                                                  .Include(x => x.StockAccessory)
                                                 .Skip(recordsToSkip)
                                                 .Take(filter.PageSize)
                                                 .AsNoTracking().ToListAsync();

                return _mapper.Map<IEnumerable<Types.StockItem>>(results);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task<Types.StockItem> GetAsync(int id)
        {
            try
            {
                var record = await _context.StockItem.Include(x => x.Image).Include(x => x.StockAccessory).AsNoTracking().FirstAsync(x => x.Id == id);

                return _mapper.Map<Types.StockItem>(record);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task<IEnumerable<Types.StockItem>> FindAsync(Types.StockItem searchItem, Types.PaginationFilter filter = null)
        {
            try
            {
                if (filter == null)
                {
                    filter = new Types.PaginationFilter { PageNumber = 1, PageSize = 1000 };
                }
                var recordsToSkip = (filter.PageNumber - 1) * filter.PageSize;
                List<StockItem> results = await _context.StockItem.Include(x => x.Image)
                                                                  .Include(x => x.StockAccessory)
                                                                  .Where(x => x.Colour == searchItem.Colour ||
                                                                              x.CostPrice == searchItem.CostPrice ||
                                                                              x.Kms == searchItem.Kms ||
                                                                              x.Make == searchItem.Make ||
                                                                              x.Model == searchItem.Model ||
                                                                              x.ModelYear == searchItem.ModelYear ||
                                                                              x.RegNo == searchItem.RegNo ||
                                                                              x.RetailPrice == searchItem.RetailPrice ||
                                                                              x.Vin == searchItem.Vin)
                                                                  .Skip(recordsToSkip)
                                                                  .Take(filter.PageSize)
                                                                  .AsNoTracking()
                                                                  .ToListAsync();

                return _mapper.Map<IEnumerable<Types.StockItem>>(results);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task AddAsync(Types.StockItem stockItem)
        {
            try
            {
                var dto = _mapper.Map<StockItem>(stockItem); 
                _context.Entry(dto).State = EntityState.Added;
                await _context.SaveChangesAsync();

                foreach (var img in dto.Image)
                {
                    img.StockItemId = dto.Id;
                    _context.Entry(img).State = EntityState.Added;
                }

                foreach (var accessory in dto.StockAccessory)
                {
                    accessory.StockItemId = dto.Id;
                    _context.Entry(accessory).State = EntityState.Added;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task AddAsync(Types.StockAccessory stockAccessory)
        {
            try
            {
                var dto = _mapper.Map<StockAccessory>(stockAccessory);
                _context.Entry(dto).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task AddAsync(Types.Image image)
        {
            try
            {
                var dto = _mapper.Map<Image>(image);
                _context.Entry(dto).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task UpdateAsync(Types.StockItem stockItem)
        {
            try
            {
                var record = await _context.StockItem.AsNoTracking().FirstAsync(x => x.Id == stockItem.Id);
                stockItem.Dtcreated = record.Dtcreated;
                stockItem.Dtupdated = DateTime.Now;
                var entity = _mapper.Map(stockItem, record, typeof(Types.StockItem), typeof(StockItem));
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                foreach (var img in stockItem.Image)
                {
                    var imgRecord = await _context.Image.AsNoTracking().FirstAsync(x => x.StockItemId == stockItem.Id && x.Id == img.Id);
                    var imgEntity = _mapper.Map(img, imgRecord, typeof(Types.Image), typeof(Image));
                    _context.Entry(imgEntity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();

                foreach (var accessory in stockItem.StockAccessory)
                {
                    var accessoryRecord = await _context.StockAccessory.AsNoTracking().FirstAsync(x => x.StockItemId == stockItem.Id && x.Id == accessory.Id);
                    var accessoryEntity = _mapper.Map(accessory, accessoryRecord, typeof(Types.StockAccessory), typeof(StockAccessory));
                    _context.Entry(accessoryEntity).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task DeleteAsync(int stockItemId)
        {
            try
            {
                var record = await _context.StockItem.Include(x => x.Image).Include(x => x.StockAccessory).FirstAsync(x => x.Id == stockItemId);
                _context.Entry(record).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task DeleteAccessoryAsync(int stockItemAccessoryId)
        {
            try
            {
                var record = await _context.StockAccessory.FirstAsync(x => x.Id == stockItemAccessoryId);
                _context.Entry(record).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task DeleteImageAsync(int stockItemImageId)
        {
            try
            {
                var record = await _context.Image.FirstAsync(x => x.Id == stockItemImageId);
                _context.Entry(record).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }
    }
}
