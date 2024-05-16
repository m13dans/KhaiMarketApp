import { ReviewsDto } from './ReviewsDto';

export interface ProductDto {
  id: number;
  name: string;
  description: string;
  stock: number;
  material: string;
  imageUrl: string;
  price: number;
  category: string;
  productBrand: string;
  reviewsDTO: ReviewsDto[];
  totalStars: number;
}
